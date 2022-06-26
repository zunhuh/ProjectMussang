using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameManage gameManage;
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
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        State_Start(State.idle);
        target = GameObject.Find("Hero");

    }

    void Update()
    {
        Debug.Log((float)CurHp / MaxHp);
        Debug.Log(CurHp);
        State_Update();
        gameManage.Boss_hp((float)CurHp / MaxHp);
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
                stateTime = Time.time + 0.7f;
                SetAnim("e1_idle");
                break;
            case State.walk:
                speed = 4; 
                SetDirection(target.transform.position);
                SetAnim("e1_walk");
                break;
            case State.rush:
                
                
                SetDirection(target.transform.position);
                speed = 7;
                stateTime = Time.time + 2.0f;
                SetAnim("e1_attack");

                break;
            case State.jump:
                SetDirection(target.transform.position);
                speed = 3;
                stateTime = Time.time + 1.5f;
                rb.AddForce(Vector3.up * 6.5f, ForceMode.Impulse);
                SetAnim("e1_attack");
                break;
            case State.hit:
                CurHp -= _param; 
                SetAnim("e1_hit"); stateTime = Time.time + 0.5f;
                break;
            case State.delay:

                break;
            case State.die:
                door.count_cur += 1; Destroy(gameObject);
                break;
        }
    }
    public void State_Update()  //state 체크 
    {
        switch (state)
        {
            case State.idle: 
                if (stateTime < Time.time)
                {
                    if (Distance() > 4) State_Start(State.rush);
                    if (Distance() < 2) State_Start(State.jump);
                    if (Distance() > 2 && Distance() < 7) State_Start(State.walk);
                }
                break;
            case State.walk:
                Move(); if (Distance()<2) State_Start(State.jump);
                if (Distance() > 4) State_Start(State.rush);
                break;
            case State.rush:
                Move();
                
                if (stateTime < Time.time) State_Start(State.idle);
                

                break;
            case State.jump:
                Move(); 
                if (stateTime < Time.time) State_Start(State.idle);
                break;
            case State.hit:
                if (CurHp <= 0) { State_Start(State.die); }
                if (stateTime < Time.time) State_Start(State.idle);
                break;
            case State.delay:

                break;

            case State.die:

                break;
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Contains("weapon"))
        {
            int dag = target.gameObject.GetComponent<Hero>().Attack_Power();
            State_Start(State.hit, dag);
        }
    }

}
