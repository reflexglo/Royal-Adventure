using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string hero_name;
    public int currency;
    public int experience;
    public int high_stage;
    public int tot_kills;
    public int tot_stages;
    public string upgrades;

    public Player(string info)
    {
        //Player_Info_String = name + currency + experience + highest stage + total kills + total stages + upgrades
        string[] attr = info.Split(",");
        hero_name = attr[0];
        currency = int.Parse(attr[1]);
        experience = int.Parse(attr[2]);
        high_stage = int.Parse(attr[3]);
        tot_kills = int.Parse(attr[4]);
        tot_stages = int.Parse(attr[5]);
        upgrades = attr[6];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getUpgradeInfo(int ind)
    {
        if(ind == 0)
        {
            string hero_class = upgrades.Split(",")[0];
            if(hero_class.Equals("W"))
            {
                return "Mage";
            }
            else if (hero_class.Equals("K"))
            {
                return "Knight";
            }
            else if (hero_class.Equals("T"))
            {
                return "Thief";
            }
        }
        return "";
    }

    public void tryUpgrade(string up, Button button)
    {
        string[] info = up.Split(",");
        if(int.Parse(info[1]) > currency)
        {
            return;
        }
        else
        {

        }
    }

}
