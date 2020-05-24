using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFairyM : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.GetInstance._health.HealHP(20, false);
            Destroy(this.gameObject);
        }
    }
}
