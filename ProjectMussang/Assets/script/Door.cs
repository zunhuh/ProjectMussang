using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door: MonoBehaviour
{
    public int count_cur = 0;

    public GameObject enemy;

    public GameManage GameManage;

    public Transform[] spawn_pos;



    void Start()
    {
        foreach (var pos in spawn_pos)
        {
            GameObject go = Instantiate(enemy, pos);
            go.GetComponent<Enemy>().door = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

  
    }
    public void OnTriggerEnter(Collider other)
    {
        if(count_cur == spawn_pos.Length)
        {

            GameManage.RoomChange();
        }
    }

}
