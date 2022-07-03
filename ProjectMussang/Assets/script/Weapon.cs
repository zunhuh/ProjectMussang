using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Hero hero;
    
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero").GetComponent < Hero >();
        sr = this.GetComponent<SpriteRenderer>();
        if (hero.direction == Direction.left) sr.flipX = false;
        else if (hero.direction == Direction.right) sr.flipX = true;
        Destroy(this.gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
