using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public int maxEnemyH = 1;
    int currentEnemyH;
    public Animator animator;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyH = maxEnemyH;
        dead = false;
    }
    
    public void TakeDamage(int damageAmount)
    {
        currentEnemyH -= damageAmount;
        if (currentEnemyH <=0)
        {
            GameplayManager.howManyCoins = GameplayManager.howManyCoins + 2;
            Die();
        }
    }

    void Die()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        if (SceneManager.GetActiveScene().buildIndex == 4)
            animator.Play("DeadGoblin");
        else if (SceneManager.GetActiveScene().buildIndex == 3)
            animator.Play("Dying Fungus");
        if (GetComponent<EnemyMovement>() != null)
            GetComponent<EnemyMovement>().enabled = false; 
        animator.SetTrigger("isDead");
        this.enabled = false;
        dead = true;
    }
}
