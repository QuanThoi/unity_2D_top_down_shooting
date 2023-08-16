using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Tim, duy chuyen va tan cong player
    public float moveSpeed = 3f;
    public int tagertRage = 2;
    public int attackRage = 1;
    public float attackSpeed = 1;

    public GameObject bullet;

    private Transform playerTransform;

    private float attackCoolDownRate;
    private float currentAttackCoolDown;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        attackCoolDownRate = 1 / attackSpeed;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        MoveToTagert(distanceToPlayer);
        Attack(distanceToPlayer);
        attackCoolDownRate -= Time.deltaTime;
    }

    private void MoveToTagert(float distance) {
        if (distance < tagertRage)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void Attack(float distance) {
        if (distance < attackRage && currentAttackCoolDown <= 0)
        {
            GameObject bulletTemp = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
            Rigidbody2D bulletTempRigidBody2D = bulletTemp.GetComponent<Rigidbody2D>();
            Vector3 direction = playerTransform.position - transform.position;
            bulletTempRigidBody2D.AddForce(direction.normalized * 10, ForceMode2D.Impulse);

            currentAttackCoolDown = attackCoolDownRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Vung se tu tagert player
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, tagertRage);

        //Vung se tu tan cong player
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRage);
    }
}
