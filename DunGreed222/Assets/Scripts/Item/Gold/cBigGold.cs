using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBigGold : MonoBehaviour
{
    //자기자신의 골드
   int _Gold;
    public GameObject _GoldText;
    AudioSource _Audio;
    AudioClip _Clip;
    private void Start()
    {
        //_Audio = FindObjectOfType<cMapManager>().GetComponent<AudioSource>();
        //_Clip = Resources.Load<AudioClip>("Sound/coin");
        //_Audio.clip = _Clip;
        _Gold = 100;
 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //_Audio.Play();
               GameObject Dam = Instantiate(_GoldText);
            Dam.transform.position = Player.GetInstance.transform.position;
            Dam.GetComponent<cText>().SetGold(_Gold);
            cGameManager.GetInstance.Gold += _Gold;
            cGameManager.GetInstance._DeleGateGold();

            Destroy(this.gameObject);
        }
    }
}
