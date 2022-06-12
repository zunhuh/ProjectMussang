using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    //스탯
    int MaxHp = 100;
    int CurHp = 100;
    int atk = 50;
    int atk_boost = 0;
    float atk_boost_time;
       

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
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        hp_bar.value = (float) CurHp / MaxHp;
        State_Update();
        Stat_Update();
        Attack();
        Jump();

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
 
    }

    void Attack()
    {

        if (Input.GetKeyDown(KeyCode.F))
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

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        if (state != State.walk && state != State.jump ) return;

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
                break;
            case State.attack:
                if (_state == State.walk) return;
                if (_state == State.jump) return;

                break;
            case State.hit:
                if (_state == State.walk) return;
                if (_state == State.jump) return;
                if (_state == State.attack) return;
                if (_state == State.hit) return;
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
                Instantiate(weapon, transform);
                stateTime = Time.time + 0.5f;
                break;
            case State.hit:
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
                break;
            case State.jump:
                break;
            case State.attack:
                if (Time.time > stateTime) State_Start(State.idle);
                break;
            case State.hit:
                if (Time.time > stateTime) State_Start(State.idle);
                break;
        }

    }
    public void Stat_Update()
    {
        atk_boost_time -= 1*Time.deltaTime;
        if (atk_boost_time <= 0) atk_boost = 0;
    }
    public int Attack_Power()
    {
        return atk + atk_boost;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("ground"))
        {
            State_Start(State.idle);
        }
        if (collision.gameObject.name.Contains("enemy"))
        {            
            int e_atk = collision.gameObject.GetComponent<Enemy>().atk;
            State_Start(State.hit, e_atk);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "item")
        {
            Item item = col.GetComponent<Item>();
            switch (item.item_index)
            {
                case (int)ItemType.hp_potion:
                    CurHp = CurHp + (int)(MaxHp * 0.5f);
                    if (CurHp > MaxHp) CurHp = MaxHp;
                    

                    break;
                case (int)ItemType.power_potion:
                    if (atk_boost_time == 0) atk_boost += (int)(atk * 0.5f);
                    atk_boost_time += 60;
                    break;
            }
            Destroy(col.gameObject);
        }
    }
}