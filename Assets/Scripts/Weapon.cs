using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int minDamege = 5;
    public int maxDamege = 10;
    public float fireRate = 1f;
    public float bulletForce;

    public BulletPlayer bullet;
    public GameObject muzzle;
    public Transform firePosion;

    private float fireRateTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        fireRateTime -= Time.deltaTime;

        if (fireRateTime < 0 && Input.GetMouseButton(0)) {
            FireBullet();
        }
    }

    public void Rotate() { 
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePosition - transform.position;

        float angle = Mathf.Atan2(lookDir.y , lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void FireBullet()
    {
        fireRateTime = fireRate;
        bullet.damage = Random.Range(minDamege, maxDamege);
        GameObject tempBullet = Instantiate(bullet.gameObject, firePosion.position, Quaternion.identity);

        //Effect
        Instantiate(muzzle, firePosion.position, transform.rotation, transform);

        Rigidbody2D rigidBody2DTempBullet = tempBullet.GetComponent<Rigidbody2D>();
        rigidBody2DTempBullet.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }
}
