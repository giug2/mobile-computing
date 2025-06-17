using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Enemy"||collision.transform.tag=="Boss1")
        {
            HealthManager.health--;
            if(HealthManager.health<=0)
            {
                GameplayManager.isGameOver = true;
                GameplayManager.howManyCoins = 0;
                AudioManager.instance.Play("GamesOver");
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }

        if(collision.transform.tag=="DeathFall")
        {
            HealthManager.health = 0;
            GameplayManager.isGameOver = true;
            AudioManager.instance.Play("GamesOver");
            gameObject.SetActive(false);
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6,7);
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
