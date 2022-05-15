using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public SpriteRenderer sp_renderer;
    public GameObject target;
    public float speed;
    float stateTime = 0;
    public enum State
    {
        idle,
        walk,
        hit
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

    public void State_Start(State _state)   //state 변경 //이벤트
    {
        state = _state;
        switch (state)
        {
            case State.idle:
                SetAnim("e1_idle");
                break;
            case State.walk:
                SetAnim("e1_walk"); SetDirection(target.transform.position);
                break;
            case State.hit:
                Debug.Log("A");
                SetAnim("e1_hit"); stateTime = Time.time + 0.5f;
                break;
        }
    }
    public void State_Update()  //state 체크 
    {
        switch (state)
        {
            case State.idle:
                if (Vector3.Distance(transform.position, target.transform.position) < 6) State_Start(State.walk);
                break;
            case State.walk:
                Move(); if (Vector3.Distance(transform.position, target.transform.position) > 9) State_Start(State.idle);
                break;
            case State.hit:
                if (stateTime < Time.time) State_Start(State.idle);
                break;
        }

    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name.Contains("weapon"))
        {
            print("aaa");
            State_Start(State.hit);
        }
    }
}
