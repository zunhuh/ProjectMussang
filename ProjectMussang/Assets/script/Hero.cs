using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    left,
    right,
}

public class Hero : MonoBehaviour
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

    public float speed = 3;
    public float jumppower = 10;
    public Direction direction = Direction.left;
    public Rigidbody rb;
    public SpriteRenderer sp_renderer;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        State_Update();

        if (state == State.walk || state != State.jump)
        {
            if (Input.GetKey(KeyCode.A))
            {
                direction = Direction.left;
                State_Start(State.walk);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = Direction.right;
                State_Start(State.walk);
            }
            else if (state == State.walk)
            {
                State_Start(State.idle);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            State_Start(State.jump);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        if (state != State.walk && state != State.jump) return;

        if (Input.GetKey(KeyCode.A))
        {
            sp_renderer.flipX = true;
            rb.MovePosition(transform.position + speed * Vector3.left * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            sp_renderer.flipX = false;
            rb.MovePosition(transform.position + speed * Vector3.right * Time.deltaTime);
        }
    }

    public void SetAnim(string s)
    {
        animator.Play(s);
    }

    public void State_Start(State _state)   //state 변경 //이벤트
    {
        state = _state;
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
                
                break;
            case State.hit:
                SetAnim("Hero_hit");
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
                break;
            case State.jump:
                break;
            case State.attack:
                break;
            case State.hit:
                break;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("ground"))
        {
            State_Start(State.idle);
        }
        if (collision.gameObject.name.Contains("enemy"))
        {
            State_Start(State.hit);
        }
    }
}