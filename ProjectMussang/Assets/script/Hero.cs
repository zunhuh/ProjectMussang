using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Direction
{
    left,
    right,
}

 public partial class Hero : MonoBehaviour
{
    public enum State
    {
        idle,
        walk,
        jump,
        attack,
        hit
    }
       

    State state = State.idle;  //이벤트(순간), 상태(지속)
    float stateTime = 0;
    public float speed = 3;
    public float jumppower = 10;
    public Direction direction = Direction.left;
    public Rigidbody rb;
    public SpriteRenderer sp_renderer;
    public Animator animator;
    public GameObject weapon;
    public Slider hp_bar;
    public BlinkDamage blinkDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        hp_bar.value = (float) CurHp / MaxHp;
        State_Update();
        Attack();
        Jump();
        Walk();
        Stat_Update();

           
    }

    void Attack()
    {

        if (Input.GetKeyDown(KeyCode.F) )
        {
            State_Start(State.attack);
        }
    }

    void Jump()
    {
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            State_Start(State.jump);
        }
    }
    void Walk()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) State_Start(State.walk);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    public void Move()
    {

        if (Input.GetKey(KeyCode.A))
        {
            sp_renderer.flipX = true;
            rb.MovePosition(transform.position + speed * Vector3.left * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            sp_renderer.flipX = false;
            rb.MovePosition(transform.position + speed * Vector3.right * Time.deltaTime);
        }
        else if(state == State.walk)
        {
            State_Start(State.idle);
        }

        
    }

    public void SetAnim(string s)
    {
        animator.Play(s);
    }

    public void State_Start(State _state, int _param = 0)   //state 변경 //이벤트
    {
        switch (state)
        {
            case State.idle:
                break;
            case State.walk:
                
                break;
            case State.jump:
                if (_state == State.attack) return;
                if (_state == State.jump) return;
                if (_state == State.walk) return;
                break;
            case State.attack:
                if (_state == State.walk) return;
                if (_state == State.attack) return;
                if (_state == State.jump) return;
                

                break;
            case State.hit:
                if (_state == State.walk) return;
                if (_state == State.jump) return;
                if (_state == State.hit) return;
                if (_state == State.attack) return;
                break;
        }
        




        state = _state;
        stateTime = Time.time;
        switch (state)
        {
            case State.idle:
                SetAnim("Hero_idle");
                break;
            case State.walk:
                SetAnim("Hero_walk");
                break;
            case State.jump:
              
                SetAnim("Hero_jump");
                rb.AddForce(Vector3.up * jumppower, ForceMode.Impulse);
                break;
            case State.attack:
                
                Instantiate(weapon, transform) ;
                stateTime = Time.time + 1.5f;
                break;
            case State.hit:
                blinkDamage.Blink_start();
                CurHp -= _param;
                SetAnim("Hero_hit");
                stateTime = Time.time + 0.5f;
                break;
        }
    }
    public void State_Update()  //state 체크 
    {
        switch (state)
        {
            case State.idle:
                break;

            case State.walk:
                Move();
                break;
            case State.jump:
                Move();
                break;
            case State.attack:
                if (Time.time > stateTime) State_Start(State.idle);
                break;
            case State.hit:
                if (Time.time > stateTime) State_Start(State.idle);
                break;
        }

    }
    public void Damage(int i)
    {
        State_Start(State.hit,i );
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("ground"))
        {
            State_Start(State.idle);
            
        }


        if (collision.gameObject.name.Contains("boss1"))
        {
            int e_atk = collision.gameObject.GetComponent<Boss>().atk;
            State_Start(State.hit, e_atk);
        }
    }
}