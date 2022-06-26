using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum Direction
    {
        left,
        right,
    }

    int MaxHp = 500;
    int CurHp = 500;
    public int atk = 40;

    public Rigidbody rb;
    public Animator animator;
    public SpriteRenderer sp_renderer;
    public GameObject target;
    public Door door;
    public float speed;
    float stateTime = 0;
    public enum State
    {
        idle,
        walk,
        rush,
        jump,
        hit,
        delay,
        die
    }
    State state = State.idle;
    Direction direction = Direction.left;
    // Start is called before the first frame update
    void Start()
    {
        State_Start(State.idle);
        target = GameObject.Find("Hero");

    }

    void Update()
    {
        State_Update();

    }
    public void SetAnim(string s)
    {
        animator.Play(s);
    }
    void SetDirection(Vector3 vector)
    {
        if (transform.position.x - vector.x > 0) { direction = Direction.left; sp_renderer.flipX = false; }
        if (transform.position.x - vector.x < 0) { direction = Direction.right; sp_renderer.flipX = true; }
    }
    private void Move()
    {
        if (direction == Direction.left) rb.MovePosition(transform.position + speed * Vector3.left * Time.deltaTime);
        if (direction == Direction.right) rb.MovePosition(transform.position + speed * Vector3.right * Time.deltaTime);
    }
    public float Distance()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public void State_Start(State _state, int _param = 0)   //state 변경 //이벤트
    {
        state = _state;
        switch (state)
        {
            case State.idle:
                Debug.Log(state);
                SetAnim("e1_idle");
                break;
            case State.walk:
                Debug.Log(state);
                speed = 4; SetDirection(target.transform.position);
                SetAnim("e1_walk");
                break;
            case State.rush:
                stateTime = Time.time + 4.0f;
                Debug.Log(state);
                speed = 7; 
                SetAnim("e1_attack");

                break;
            case State.jump:
                Debug.Log(state);
                stateTime = Time.time + 2.0f;
                rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
                SetAnim("e1_attack");
                break;
            case State.hit:
                CurHp -= _param; if (CurHp <= 0) { State_Start(State.die); break; }
                SetAnim("e1_hit"); stateTime = Time.time + 0.5f;
                break;
            case State.delay:

                break;
            case State.die:
                door.count_cur += 1; Destroy(this.gameObject);
                break;
        }
    }
    public void State_Update()  //state 체크 
    {
        switch (state)
        {
            case State.idle:
                if (Distance() > 7) State_Start(State.rush);
                if (Distance() < 2) State_Start(State.jump);
                if (Distance() > 2 && Distance() < 7) State_Start(State.walk);
                break;
            case State.walk:
                Move(); if (Distance()<2) State_Start(State.jump);
                if (Distance() > 7) State_Start(State.rush);
                break;
            case State.rush:
                Move();  State_Start(State.idle);
                if (stateTime < Time.time) State_Start(State.idle);
                

                break;
            case State.jump:
                if (stateTime < Time.time) State_Start(State.idle);
                break;
            case State.hit:

                break;
            case State.delay:

                break;

            case State.die:

                break;
        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        
    }

}
