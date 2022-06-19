using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Hero : MonoBehaviour
{

    //Ω∫≈»
    int MaxHp = 100;
    int CurHp = 100;
    int atk = 50;
    int atk_boost = 0;
    float atk_boost_time=0;
    public List<int> buff_list = new List<int>();
    public Image[] buff_slot;
    int slot_num =0;



    public int Attack_Power()
    {
        return atk + atk_boost;
    }

    public void Stat_Update()
    {
        atk_boost_time -= 1 * Time.deltaTime;
        if (atk_boost_time <= 0)
        {
            buff_list.Remove((int)ItemType.power_potion);
            atk_boost = 0;
        }
    }

    public void Display_Buffslot()
    {
        slot_num = 0;
        foreach (var item in buff_list)
        {
            if (item == (int)ItemType.power_potion) buff_slot[slot_num].sprite =Resources.Load<Sprite>("item/item_02");
            slot_num++;
        }

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "item")
        {
            Item item = col.GetComponent<Item>();
            switch (item.item_index)
            {
                case (int)ItemType.hp_potion:
                    CurHp = CurHp + (int)(MaxHp * 0.5f);
                    if (CurHp > MaxHp) CurHp = MaxHp;
                    break;
                case (int)ItemType.power_potion:
                    buff_list.Add((int)ItemType.power_potion);
                    atk_boost += 20;
                    atk_boost_time += 30;
                    Display_Buffslot();
                    break;
            }
            Destroy(col.gameObject);
        }
    }
}