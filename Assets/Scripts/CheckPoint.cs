using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag=="Player")
        {
            //PlayerMovement.lastCheckPointPost = transform.position;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
