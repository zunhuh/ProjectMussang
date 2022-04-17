using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    idle,
    run, 
    jump 
}
public enum Dirction
{
    left,
    right,
}

public class Hero : MonoBehaviour
{
    State state = State.idle;  //이벤트(순간), 상태(지속)

    public float speed = 3;
    public float jumppower = 10;
    public Dirction direction = Dirction.left;
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

        if (state == State.run || state != State.jump)
        {
            if (Input.GetKey(KeyCode.A))
            {
                direction = Dirction.left;
                State_Start(State.run);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = Dirction.right;
                State_Start(State.run);
            }
            else if (state == State.run)
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
        if (state != State.run && state != State.jump) return;

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
                break;
            case State.run:
                break;
            case State.jump:
                rb.AddForce(Vector3.up * jumppower, ForceMode.Impulse);
                break;
        }
    }
    public void State_Update()  //state 체크 
    {
        switch (state)
        {
            case State.idle:
                break;
            case State.run:
                break;
            case State.jump:
                break;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("ground"))
        {
            State_Start(State.idle);
        }
    }
}