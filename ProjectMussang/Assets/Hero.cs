using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    idle,
    run,
    jump
}


public class Hero : MonoBehaviour
{
    State state = State.idle;

    public float speed = 3;
    public float jumppower = 10;
    public Rigidbody rb;
    public SpriteRenderer sp_renderer;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (state != State.jump)
        {
            if (Input.GetKey(KeyCode.A))
            {
                state = State.run;
            }
            if (Input.GetKey(KeyCode.D))
            {
                state = State.run;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(state == State.run)
            Move();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.jump;
            rb.AddForce(Vector3.up * jumppower, ForceMode.Impulse);
        }
    }
    public void Move()
    {
        if (Input.GetKey(KeyCode.A)) 
        {
            rb.MovePosition(transform.position+speed*Vector3.left*Time.deltaTime);
            sp_renderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + speed * Vector3.right * Time.deltaTime);
            sp_renderer.flipX = false;
        }       
    }
    public void SetAnim(string s)
    {
        animator.Play(s);
    }
}
