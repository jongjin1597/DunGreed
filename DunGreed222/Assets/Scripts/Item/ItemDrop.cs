using UnityEngine;
using System.Collections;


public class ItemDrop : MonoBehaviour
{
    AudioSource _Audio;
    AudioClip _Clip;
    //현재 아이템
    public Item _item;
    //플레이어충돌여부
    private bool _isPlayer=false;

    public GameObject _ButtonF;


    private Rigidbody2D _rigid;

    private void Awake()
    {
        _ButtonF = transform.GetChild(0).gameObject;
        _rigid = GetComponent<Rigidbody2D>();
        _Audio = Player.GetInstance.GetComponent<AudioSource>();
        _Clip = Resources.Load<AudioClip>("Sound/GetItem");
     
        _rigid.velocity = new Vector2( 0 ,5.0f);
    }


    private void Update()
    {
        if (_isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _Audio.clip = _Clip;
                _Audio.Play();
                cInventory.GetInstance.AddItem(_item);
                Destroy(this.gameObject);
            }
        }

    }

    public void SetItem(Item item)
    {
        _item = item;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Player")
        {
            _ButtonF.SetActive(true);
            _isPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _ButtonF.SetActive(false);
            _isPlayer = false;
        }
    }
}
