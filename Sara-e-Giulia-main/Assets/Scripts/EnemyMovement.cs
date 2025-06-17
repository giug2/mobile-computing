using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 0.8f;
    public float range;
    private bool isGoingRight=true;
    public Transform groundDetector;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * range);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, range);
        if (groundInfo.collider == false)
        {
            if (isGoingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isGoingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isGoingRight = true;
            }
        }
    }
}
