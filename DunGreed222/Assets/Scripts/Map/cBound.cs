using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBound : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            cCameramanager.GetInstance.SetBound(collision);
            this.transform.parent.GetComponent<cMap>().SetPlayer(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.transform.parent.GetComponent<cMap>().SetPlayer(false);
        }
    }
}
