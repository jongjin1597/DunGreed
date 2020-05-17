using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDogBox : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
 
        //피격판정및 공격판정 스타트용
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<SkelDog>().Attack();
        }
    }
}

