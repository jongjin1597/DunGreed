using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGameStart : MonoBehaviour
{
    public GameObject DungeonEat;
    private Animator DunGeonAnim;

    private void Awake()
    {
        DunGeonAnim = DungeonEat.GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

            DungeonEat.transform.position = new Vector3(Player.GetInstance.transform.position.x, DungeonEat.transform.position.y, 0);
            DunGeonAnim.SetTrigger("Open");

            cUIManager.GetInstance.gameObject.SetActive(false);
            Player.GetInstance._Ani.StopPlayback();
            Player.GetInstance.MoveMap = true;
            Player.GetInstance._Rigidbody.velocity = Vector3.zero;
            //cSceneManager.GetInstance.ChangeScene("Play", null, 2);
          
        }
    }

}

