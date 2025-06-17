using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameplayManager.howManyCoins++;
            Destroy(gameObject);
            AudioManager.instance.Play("Coins");
        }
    }
}
