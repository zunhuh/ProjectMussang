using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{

    private static GameManage instance = null;
    public static GameManage Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(GameManage)) as GameManage;

            }
            return instance;
        }
    }

    public Hero hero;
    public Text room_text;
    private int floor;
    private int room;
    public Slider Boss_hp_bar;
    //sound
    public GameObject A_warning;
    public AudioSource A_bgm;
    public bool isBattle;
    public GameObject clearPanel;
    public GameObject boss_panel;





    // Start is called before the first frame update
    void Start()
    {
        clearPanel.SetActive(false);
        boss_panel.SetActive(false);
        isBattle = true;
        Boss_hp_bar.gameObject.SetActive(false);
        A_bgm.Play();
        
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

    public void Boss_hp(float a)
    {
        Boss_hp_bar.value = a;
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
            map_num += room * 1000;
            GameObject msp_gb = GameObject.Find(map_num.ToString());
            print(map_num);
        }



    }
    public void RoomChange()
    {   
        room += 1;
        hero.transform.Translate(10, 0, 0);
        //보스HP표시
        if(room == 2) 
        {
            StartCoroutine("BossWarning");
        }
       if (room != 2) { Boss_hp_bar.gameObject.SetActive(false); }
    }
    public void loadScene()
    {   

        clearPanel.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("PlayScene");
       
    }

    IEnumerator BossWarning()
    {
        boss_panel.SetActive(true);
        isBattle = false;
        Instantiate(A_warning);

        yield return new WaitForSeconds(2.0f);

       Boss_hp_bar.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        boss_panel.SetActive(false);
        isBattle = true;
    }
}