using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBulletChild : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<cBullet>().OnTriggerEnter2D(collision);

    }
    public void CrashBullet()
    {
        this.transform.parent.GetComponent<cBullet>().CrashBullet();
    }
}
