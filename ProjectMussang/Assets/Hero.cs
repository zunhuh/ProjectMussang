using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float speed = 3;
    public float jumppower = 10;
    public Rigidbody rb;
    public SpriteRenderer sp_renderer;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        if (Input.GetKey(KeyCode.A)) 
        {
            rb.MovePosition(transform.position+speed*Vector3.left*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + speed * Vector3.right * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up*jumppower,ForceMode.Impulse);
        }
    }
}
