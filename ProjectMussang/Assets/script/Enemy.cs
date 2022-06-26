using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    //스탯
    int MaxHp = 100;
    int CurHp =100;
    public int atk = 20;

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
        attack,
        hit,
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

    // Update is called once per frame
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
        if (transform.position.x - vector.x < 0) {direction = Direction.right; sp_renderer.flipX = true;}
    }
    private void Move()
    {   
        if(direction == Direction.left) rb.MovePosition(transform.position + speed * Vector3.left * Time.deltaTime);
        if (direction == Direction.right) rb.MovePosition(transform.position + speed * Vector3.right * Time.deltaTime);
    }

    public void State_Start(State _state, int _param = 0)   //state 변경 //이벤트
    {
        state = _state;
        switch (state)
        {
            case State.idle:
                SetAnim("e1_idle");
                break;
            case State.walk:
                speed = 2; SetDirection(target.transform.position);
                SetAnim("e1_walk"); 
                break;
            case State.attack:
                speed = 5;
                SetAnim("e1_attack"); 
                 
                break;
            case State.hit:
                CurHp -= _param; if (CurHp <= 0) { State_Start(State.die); break; }
                SetAnim("e1_hit"); stateTime = Time.time + 0.5f;
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
                if (Vector3.Distance(transform.position, target.transform.position) < 5) State_Start(State.walk);
                break;
            case State.walk:
                 Move(); if (Vector3.Distance(transform.position, target.transform.position) > 5) State_Start(State.idle);
                if (Vector3.Distance(transform.position, target.transform.position) < 2) State_Start(State.attack);
                break;
            case State.attack:
                Move(); if (Vector3.Distance(transform.position, target.transform.position) > 3) State_Start(State.idle);
                break;
            case State.hit:
                if (stateTime < Time.time) State_Start(State.idle);
                break;
            case State.die:

                break;
        }
    }

  

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("weapon"))
        {
            int dag = target.gameObject.GetComponent<Hero>().Attack_Power();
            State_Start(State.hit, dag);
        }
    }
}
