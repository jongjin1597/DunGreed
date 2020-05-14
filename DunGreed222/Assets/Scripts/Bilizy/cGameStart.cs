using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGameStart : MonoBehaviour
{

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Player.GetInstance._CurrentMapNum = 1;
         
            cSceneManager.GetInstance.ChangeScene("Play", null, 2);

        }
        }
  
}

