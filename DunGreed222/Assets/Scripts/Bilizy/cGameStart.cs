using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGameStart : MonoBehaviour
{
    private Player _Player;

    
    private void Awake()
    {
        _Player = FindObjectOfType<Player>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _Player._CurrentMapName = "Start";
         
            cSceneManager.GetInstance.ChangeScene("Play", null, 2);

        }
        }
  
}

