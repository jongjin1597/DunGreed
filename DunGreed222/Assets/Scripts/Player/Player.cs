using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    Idle=0,
    Move,
    Attack,
    Dash,
    Jump,
    Damage,
    Die
}
public class Player : cSingleton<Player>
{
    //현제 게이지
    public cDash _Dash;
    //현재맵
    //private cMap _NowMap;
    //싱글톤변수
    private static Player instance;

    //이동할맵이름   
    public string _CurrentMapName;
    public cHP _health;
    private float _InitHealth = 80;
    //포만감
    public cFoodGauge _Food;

    //이동
    float _horizontalMove;
    //이동속도
    public float _MaxSpeed = 3f;
    //점프파워
    public float _JumpPower = 3f;
    //캐릭터상태
    private State _state = State.Idle;
    //리지드바디
    private Rigidbody2D _Rigidbody;
    //스프라이트랜더러
    private SpriteRenderer _Renderer;
    //애니매이터
    private Animator _Animator;


    //최대대시횟수
    private int _DashCount=3;
    //최대 점프횟수
    private int _JumpCount = 2;
    //점프상태
    private bool _isJump = false;
    //대시카운트 충전용시간
    private float _Time=0f;
    //마우스포지션
    private Vector2 _MousePosition;
    public BoxCollider2D foot;

    protected override void Awake()
    {
        base.Awake();
        //_health.Initialize(_InitHealth, _InitHealth);

        _health.Initialize(_InitHealth, _InitHealth);
        _Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        _Animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //이동
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        //점프상태
        if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.S))
        {
            _isJump = true;
        }
        
        AnimationUpdate();
        //hp확인
        if (Input.GetKeyDown(KeyCode.O))
        {
            _health.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            _health.MyCurrentValue += 10;
        }

        //마우스포지션 불러오기
        _MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    
        //마우스좌표에따라 캐릭터 회전
        if (_MousePosition.x < transform.position.x)
        {
            _Renderer.flipX = true;
        }
        else if (_MousePosition.x > transform.position.x)
        {
            _Renderer.flipX = false;
        }

        //대시횟수 충전
            _Time += Time.deltaTime;
        if (_Time >= 1.0f)
        {

            if (_DashCount < 3)
            {
                _Dash.SetEnabled(_DashCount);
                _DashCount += 1;
            }
                _Time = 0;
        }

        //HP0되면 죽는것
        if (_health.MyCurrentValue == 0)
        {
            _state = State.Die;
        }       
    }

    private void FixedUpdate()
    {
        
        Debug.DrawRay(_Rigidbody.position, Vector3.down *2 , new Color(0, 1, 0));

        if (_Rigidbody.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(_Rigidbody.position, Vector3.down *2.5f, 1, LayerMask.GetMask("floor"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.2f)
                {
                    _JumpCount = 2;
                    _Animator.SetBool("Jump", false);
                }
            }
        }
        Jump();
        Move();
     
        if (Input.GetMouseButtonDown(1))
        {
            if (_DashCount == 0)
            {
                return;
            }
            StartCoroutine("Dash");
        }
    }

    void Move()
    {
        //대시상태일때 이동안됨
        if (_state == State.Dash)
        {
            return;
        }

        _state = State.Move;

        _Rigidbody.AddForce(Vector2.right * _horizontalMove*2, ForceMode2D.Impulse);
        if(_Rigidbody.velocity.x> _MaxSpeed)
        {
            _Rigidbody.velocity = new Vector2(_MaxSpeed, _Rigidbody.velocity.y);
        }
        else if (_Rigidbody.velocity.x < _MaxSpeed*(-1))
        {
            _Rigidbody.velocity = new Vector2(_MaxSpeed*(-1), _Rigidbody.velocity.y);
        }
        if(_horizontalMove ==0)
        {
            _Rigidbody.velocity = new Vector2(0, _Rigidbody.velocity.y);
        }

    }

    void Jump()
    {
        //점프상태가 아니거나 점프 횟수가 없으면 리턴
        if (!_isJump || _JumpCount == 0)
        {
            return;
        }
 
        _state = State.Jump;
        _Rigidbody.velocity = Vector3.zero;
        _Rigidbody.AddForce(Vector2.up * _JumpPower, ForceMode2D.Impulse);
        _Animator.SetBool("Jump", true);
        _JumpCount -= 1;      
        _isJump = false;
        _state = State.Idle;
    }
    IEnumerator Dash()
    {

        //대시횟수가 0이아니거나 대시상태가 아닐때 실행
        if (_state != State.Dash || _DashCount > 0)
        {
            _state = State.Dash;
           
            int m_nIndex = 0;
            Vector3 m_vecStartPoint = transform.position;
            Vector2 m_vecEndPoint = _MousePosition;
            Vector2 m_vecDirection = new Vector2(m_vecEndPoint.x - m_vecStartPoint.x, m_vecEndPoint.y - m_vecStartPoint.y).normalized;
            float m_fRange = Vector2.Distance(m_vecStartPoint, m_vecEndPoint);
            if (m_fRange >= 4.0f)
            {
                m_fRange = 4.0f;
            }

            while (m_nIndex < 4)
            {
                m_nIndex += 1;
                _Rigidbody.velocity = Vector3.zero;

                Vector2 m_vecLerp = Vector2.Lerp(m_vecStartPoint, (Vector2)m_vecStartPoint + (m_vecDirection * m_fRange), (float)m_nIndex / 4.0f);
                _Rigidbody.MovePosition(m_vecLerp);
                yield return new WaitForSeconds(0.01f);
            }
            _Dash.SetEnabledfasle(_DashCount - 1);
            _DashCount -= 1;
            _state = State.Idle;
        }
    }

    void AnimationUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            _Animator.SetBool("Run", false);
            _state = State.Idle;
        }
        else
        {
            _Animator.SetBool("Run", true);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Brige") {
            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S))
            {
                foot.enabled = false;
                Invoke("Jumper", 0.4f);
            }
        }
    }

    public void Jumper()
    {
        foot.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bound"))
        {

            cCameramanager.GetInstance.SetBound(collision);
        }
    }

}
