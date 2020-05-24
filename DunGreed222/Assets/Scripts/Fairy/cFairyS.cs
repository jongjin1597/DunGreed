using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFairyS : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            Player.GetInstance._health.HealHP(10, false);
            Destroy(this.gameObject);
        }
    }
}
