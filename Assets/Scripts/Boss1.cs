using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{
    Transform target;
    public int bossHP = 100;
    public Animator animator;
    //Boss health bar
    public Slider healthBar;
    public Gradient gradient;
    public Image fill;
    //Boss animation variables
    bool hurt;
    float nextAttackTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = bossHP;
        healthBar.value = bossHP;
        fill.color = gradient.Evaluate(1f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        hurt = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1.2f, 1.2f);
        }
        else
        {
            transform.localScale = new Vector2(1.2f, 1.2f);
        }
        if ((Time.time >= nextAttackTime)&& hurt)
        {
            animator.ResetTrigger("damage");
            hurt = false;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        bossHP -= damageAmount;
        healthBar.value = bossHP;
        fill.color = gradient.Evaluate(healthBar.normalizedValue);
        if (bossHP > 0)
        {
            animator.SetTrigger("damage");
            nextAttackTime = Time.time + 1f / 2;
            hurt = true;
        }
        else
        {
            animator.SetTrigger("death");
            GetComponent<BoxCollider2D>().enabled = false;
            this.enabled = false;
            GameplayManager.gameWon = true;
        }
    }
}
