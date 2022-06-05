using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door: MonoBehaviour
{
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 0) {this.gameObject.transform.Translate(0, -1*Time.deltaTime, 0);if (gameObject.transform.position.y <= -3) Destroy(this.gameObject); }
    }
}
