using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HealthManager.health != 3)
        {
            if (collision.transform.tag == "Player")
            {
                HealthManager.health++;
                Destroy(gameObject);
                AudioManager.instance.Play("Potion");
            }
        }
    }
}
