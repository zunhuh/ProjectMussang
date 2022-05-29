using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy1;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy1, transform);
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
