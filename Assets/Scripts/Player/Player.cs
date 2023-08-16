using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rollBoost = 5f;
    public float rollTime = 1f;

    public Vector3 moveInput;
    public Animator animator;
    public SpriteRenderer character;

    private bool isRoll = false;
    private float currentRollTime = 0;
    private Rigidbody2D rigidBody2D;

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("CharacterAnimator").GetComponent<Animator>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        animator.SetFloat("moveSpeed", moveInput.sqrMagnitude);

        //Khi nguoi choi an nut Space va nhan vat hien tai khong trong trang thai roll thi bat dau roll
        if (Input.GetKeyDown(KeyCode.Space) && currentRollTime <= 0) { 
            moveSpeed += rollBoost;
            currentRollTime = rollTime;
            isRoll = true;
            animator.SetBool("isRoll", true);
        }

        //Khi het thoi gian roll thi set lai gia tri moveSpeed va cho phep nguoi choi thuc hien roll
        if (isRoll && currentRollTime <= 0) {
            moveSpeed -= rollBoost;
            isRoll = false;
            animator.SetBool("isRoll", false);
        }
        else {
            currentRollTime -= Time.deltaTime;
        }
      

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                character.transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                character.transform.localScale = new Vector3(-1, 1, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
    }
}
