using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxDistance = 2f;
    public float repeatTime = 0.5f;
    public float scaleRate = 1f;

    //Emeny attr
    public int heath = 500;
    public int damage = 10;
    public float attackSpeed = 2f;
    public int attackRage = 10;
    public float moveSpeed = 3f;
    public float bulletSpeed = 5f;

    public const string DEATH_ANIMATION = "enemy_death";

    public UnityEvent onDeath;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public BulletEnemy bullet;
    public Image heathBar;

    int currentHeath;
    float attackRate;
    float attackCooldown;
    float xScale;
    float yScale;
    Rigidbody2D rigidBody2D;
    Coroutine moveCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentHeath = (int) (heath * scaleRate);
        bullet.damage = (int) (damage * scaleRate);
        attackRate = 1 / attackSpeed;
        xScale = 1 * scaleRate;
        yScale = 1 * scaleRate;
        spriteRenderer.transform.localScale = new Vector3(xScale, yScale, 1);
    }

    void Update()
    {
        if (heath == 0) { 
            Destroy(this.gameObject);
        }

        Attack();
    }

    Vector2 FindTargetPosition(Player _gameObject) {
        Vector3 playerPoistion = _gameObject.transform.position;
        return (Vector2)playerPoistion + (attackRage * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized);
    }

    Player FindPlayer() {
        return FindObjectOfType<Player>();
    }

    void CalculatePath() {
        Player player = FindPlayer();

        if (player == null) {
            return;
        }

        Vector2 target = FindTargetPosition(player);
    }

    void Attack()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
            return;
        }

        //Tan cong tu xa neu pham vi tan cong lon hon 2 nguoc lai tan cong can chien
        if (attackRage > 1f)
        {
            Player player = FindPlayer();

            if (player == null)
            {
                return;
            }

            GameObject bulletTemp = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
            Rigidbody2D bulletTempRigidBody2D = bulletTemp.GetComponent<Rigidbody2D>();
            Vector3 playerPosition = player.transform.position;
            Vector3 direction = playerPosition - transform.position;
            bulletTempRigidBody2D.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
            attackCooldown = attackRate;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && attackCooldown <= 0)
        {
            collision.gameObject.GetComponent<PlayerHeath>().TakeDamge(damage);
            attackCooldown = attackRate;
        }
    }

    private void UpdateHeath(int currentHeath)
    {
        if(heathBar != null) { 
            heathBar.fillAmount = (float)currentHeath / (float)heath;
        }
    }

    public void TakeDamage(int damege) {
        currentHeath -= damege;

        if (currentHeath <= 0) {
            onDeath.Invoke();
            currentHeath = 0;           
        }
        else {
            UpdateHeath(currentHeath);
        }
    }

    public void OnDeath() {
        animator.Play(DEATH_ANIMATION);
        Destroy(gameObject, 0.2f);
    }
}
