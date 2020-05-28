using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFairyM : MonoBehaviour
{
    AudioSource _Audio;
    AudioClip _Clip;
    private void Awake()
    {
        _Audio = transform.parent.parent.parent.GetComponent<AudioSource>();
        _Clip = Resources.Load<AudioClip>("Sound/Get_Fairy");
        _Audio.clip = _Clip;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          
            _Audio.Play();
            cSoundManager.GetInstance.PlayEffectSound("Sound/Get_Fairy");
            transform.parent.parent.parent.GetComponent<cMapManager>().ReMoveFairy(this.gameObject);
            Player.GetInstance._health.HealHP(20, false);
           Destroy(this.gameObject);
        }
    }
}
