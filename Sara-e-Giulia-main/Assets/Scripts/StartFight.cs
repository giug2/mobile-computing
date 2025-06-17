using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFight : MonoBehaviour
{
    public Transform fightBoundLeft;
    public Transform fightBoundRight;
    public GameObject dialogue;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameplayManager.metBoss = true;
            fightBoundLeft.GetComponent<BoxCollider2D>().enabled = true;
            fightBoundRight.GetComponent<BoxCollider2D>().enabled = true;
            dialogue.SetActive(true);
            Destroy(gameObject);
        }
    }
}
