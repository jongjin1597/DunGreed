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

            transform.parent.parent.GetComponent<cMapManager>().SetNowMap(this.transform.parent.gameObject);
            GameObject Map = transform.parent.parent.GetComponent<cMapManager>()._CloseMapList.Find(x => x == transform.parent);
            if (Map) 
            {
                transform.parent.parent.GetComponent<cMapManager>()._OpenMapList.Add(Map);
                transform.parent.parent.GetComponent<cMapManager>()._CloseMapList.Remove(Map);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.parent.GetComponent<cMapManager>().SetNowMap(null); 
        }
    }
}
