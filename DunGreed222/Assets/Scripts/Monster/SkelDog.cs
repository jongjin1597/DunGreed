using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelDog : MonoBehaviour
{
    Rigidbody2D _rigid;


    // Start is called before the first frame update
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_rigid.position, Vector3.down, new Color(0, 1, 0));

        if (_rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(_rigid.position, Vector3.down * 2.5f, 1, LayerMask.GetMask("floor"));
        }
    }
}
