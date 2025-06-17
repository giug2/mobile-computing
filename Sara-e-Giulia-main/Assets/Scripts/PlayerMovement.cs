using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //General variables
    PlayerControls controls;
    float direction = 0;
    public Rigidbody2D playerRB;
    public float speed=20f;
    public Animator animator;
    //Move control
    bool facingRight = true;
    //Jump control
    public float jumpForce = 3;
    bool isGrounded;
    int numberOfJumps = 0;
    public Transform groundCheck;
    public LayerMask groundLayer;
    //Attack control
    public Transform attackPoint;
    public float attackRange=0.5f;
    public LayerMask enemyLayer;
    public float attackRate = 2;
    float nextAttackTime = 0;

    private void Awake() {
        controls = new PlayerControls();
        controls.Enable();
        controls.Ground.Move.performed += context =>
          {
              direction = context.ReadValue<float>();
          };
        controls.Ground.Jump.performed += context => Jump();
        controls.Ground.Attack.performed += context => Attack();
    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);
        animator.SetFloat("New Float", Mathf.Abs(direction));
        if (facingRight && direction < 0|| !facingRight && direction > 0)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            numberOfJumps = 0;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            numberOfJumps++;
            AudioManager.instance.Play("FirstJump");
        }
        else
        {
            if (numberOfJumps == 1)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                numberOfJumps++;
                AudioManager.instance.Play("SecondJump");
            }
        }   
    }

    void Attack()
    {
        if(Time.time>=nextAttackTime) {
            animator.SetTrigger("Attacking");
            AudioManager.instance.Play("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.transform.tag == "Boss1")
                {
                    enemy.GetComponent<Boss1>().TakeDamage(15);
                }
                else
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(1);
                }
            }
            nextAttackTime=Time.time + 1f / attackRate; 
        }
    }
}
