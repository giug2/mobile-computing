using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyToAttack2 : StateMachineBehaviour
{
    Transform target;
    Rigidbody2D boss;
    public float speed = 1;
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
        Vector2 newPos = new Vector2(target.position.x, boss.position.y);
        Vector2 moving = Vector2.MoveTowards(boss.position, newPos, speed * Time.fixedDeltaTime);
        Flipping(animator);
        boss.MovePosition(moving);
        if (Vector2.Distance(target.position, boss.position) <= 4)
            animator.SetBool("isAttack", true);
        if (Vector2.Distance(target.position, boss.position) >= 13)
            animator.SetBool("isReady", false);
    }

    public void Flipping(Animator animator)
    {
        if (animator.transform.position.x < target.position.x && direction == 1 || animator.transform.position.x > target.position.x && direction == -1)
        {
            animator.transform.localScale = new Vector2(animator.transform.localScale.x * -1, animator.transform.localScale.y);
            direction = direction * -1;
        }
    }
}
