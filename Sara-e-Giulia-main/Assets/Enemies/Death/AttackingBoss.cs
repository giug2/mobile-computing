using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingBoss : StateMachineBehaviour
{
    Transform target;
    Rigidbody2D boss;
    public int direction = 1;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        boss = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Flipping(animator);
        float distance = Vector2.Distance(target.position, boss.position);
        if (distance > 4)
        {
            animator.SetBool("isAttack", false);
            AudioManager.instance.Play("Boss1Attack");
        }
    }

    public void Flipping(Animator animator)
    {
        if (animator.transform.position.x < target.position.x && direction == 1 || animator.transform.position.x > target.position.x && direction == -1)
        {
            animator.transform.localScale = new Vector2(animator.transform.localScale.x * -1, animator.transform.localScale.y);
            direction = direction * -1;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("isAttack");
    }
}
