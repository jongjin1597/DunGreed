using UnityEngine;
using System.Collections;


public class ItemDrop : MonoBehaviour
{   
    //현재 아이템
    public Item _item;
    //플레이어충돌여부
    private bool _isPlayer=false;

    private GameObject _ButtonF;

    private BoxCollider2D _Box;

    private Rigidbody2D _rigid;

    private void Awake()
    {
        _ButtonF = transform.GetChild(0).gameObject;
        _rigid = GetComponent<Rigidbody2D>();
        //StartCoroutine("Dash");
        _rigid.velocity = new Vector2( 0 ,5.0f);
    }


    private void Update()
    {
        if (_isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {

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
