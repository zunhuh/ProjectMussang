using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{

    public Text room_text;
    private int floor;
    private int room;




    // Start is called before the first frame update
    void Start()
    {
        floor = 1;
        room = 1;
        room_text.text = "stage: "+floor + "-" + room;
        MapSetting(1);
    }

    // 방클리어시 텍스트 변경
    // 보스 클리어시 씬전환

    // Update is called once per frame
    void Update()
    {

    }

    public void MapSetting(int floor)
    {
        int length = 5;

        List<int> overlap_list = new List<int>();
        int map_num = new int();
        bool overlap_b = true;


        for (int i = 0; i < length; i++)
        {
            map_num = Random.Range(0, length);
            if (i == 0){
                
            }
            else
            {
                while (overlap_b)
                {
                    if (overlap_list.Contains(map_num))
                    { map_num = Random.Range(0, length); }
                    else {overlap_b = false;}
                }
            }
            overlap_list.Add(map_num);

            overlap_b = true;
            map_num += stage * 1000;
            GameObject msp_gb = GameObject.Find(map_num.ToString());
            print(map_num);
        }



    }
}