using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TMP_Text name_text;
    public TMP_Text high_text;
    public TMP_Text kills_text;
    public TMP_Text stages_text;

    public TMP_Text hero_class_text;
    public TMP_Text currency_text;
    public TMP_Text health_text;
    public TMP_Text levels_text;
    public TMP_Text xp_text;
    public Slider xp_sd;

    public Slider volume_sd;
    public TMP_InputField[] control_ip_list;

    public Button play_bt;
    public Button upgrade_bt;
    public GameObject setup_player_pn;

    public GameObject hero_save_dd;
    public GameObject hero_name_ib;
    Player player;

    float volume;
    string[] controls;
    // Start is called before the first frame update
    void Start()
    {
        volume = 100;
        controls = new string[]{"W","A","S","D","M1","M2"};
        volume_sd.value = volume;
        for(int i = 0;i<controls.Length;i++)
        {
            control_ip_list[i].text = controls[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveGame()
    {
        string dir = "..\\Royal Adventure\\saves.txt";
        string[] lines = File.ReadAllLines(dir);
        int ind = -1;
        for(int i = 0;i<lines.Length;i++)
        {
            if(lines[i].Split(",")[0].Equals(player.hero_name))
            {
                ind = i;
                break;
            }
        }
        lines[ind] = player.hero_name + "," + player.currency + "," + player.experience + "," + player.high_stage + "," + player.tot_kills + "," + player.tot_stages + "," + player.upgrades;
        File.WriteAllLines(dir,lines);
    }

    public void initPlayer(bool load)
    {
        string dir = "..\\Royal Adventure\\saves.txt";
        if (load)
        {
            string hero_name = hero_save_dd.GetComponent<TMP_Dropdown>().options[hero_save_dd.GetComponent<TMP_Dropdown>().value].text;
            if (hero_name.Length > 0)
            {
                string[] lines = File.ReadAllLines(dir);
                foreach (string line in lines)
                {
                    string[] info = line.Split(",");
                    if (info[0].Equals(hero_name))
                    {
                        player = new Player(line);
                        break;
                    }
                }
            }
        }
        else
        {
            string hero_name = hero_name_ib.GetComponent<TMP_InputField>().text;
            if (hero_name.Length > 0)
            {
                string[] lines = File.ReadAllLines(dir);
                bool exists = false;
                foreach (string line in lines)
                {
                    string[] inf = line.Split(",");
                    if (inf[0].Equals(hero_name))
                    {
                        exists = true;
                        break;
                    }
                }
                if(!exists)
                {
                    StreamWriter writer = File.AppendText(dir);
                    //Player_Info_String = name + currency + experience + highest stage + total kills + total stages + upgrades
                    string info = hero_name + "," + 1000 + "," + 0 + "," + 0 + "," + 0 +"," + 0 +",";
                    writer.WriteLine(info);
                    writer.Close();
                    player = new Player(info);
                }
            }
        }
    }

    public float inverseXPFunc(float val)
    {
        return ((Mathf.Log((val+196.33333f) / 235, Mathf.Exp(1))) / (Mathf.Log(1.2f, Mathf.Exp(1)))) + 1;
    }

    public float XPFunc(float level)
    {
        return (235 * (Mathf.Pow(1.2f, level - 1))) - 196.33333f;
    }

    public int calcLevel(int xp)
    {
        return (int)Mathf.Ceil(inverseXPFunc((float)xp));
    }

    public int calcNextXP(int level)
    {
        return (int)Mathf.Ceil(XPFunc((float)level));
    }

    public void updatePlayScreen()
    {
        TMP_Text[] text_objs = { name_text, high_text, kills_text, stages_text, hero_class_text, currency_text, health_text, levels_text, xp_text };
        xp_sd.minValue = 0;
        xp_sd.maxValue = (calcNextXP(calcLevel(player.experience)) - calcNextXP(calcLevel(player.experience) - 1));
        xp_sd.value = (player.experience - calcNextXP(calcLevel(player.experience) - 1));
        text_objs[0].text = player.hero_name;
        text_objs[1].text = "Highest Stage: " + player.high_stage;
        text_objs[2].text = "Total Kills: " + player.tot_kills;
        text_objs[3].text = "Total Stages: " + player.tot_stages;
        text_objs[4].text = "Class: " + player.getUpgradeInfo(0);
        text_objs[5].text = "Royals: " + player.currency;
        text_objs[6].text = "Max Health: " + player.getUpgradeInfo(1);
        text_objs[7].text = "Level: " + (calcLevel(player.experience));
        text_objs[8].text =  xp_sd.value + "/" + xp_sd.maxValue + " XP";
        if (player.upgrades.Length == 0)
        {
            play_bt.interactable = false;
            upgrade_bt.interactable = false;
            setup_player_pn.SetActive(true);
        }
        else
        {
            play_bt.interactable = true;
            upgrade_bt.interactable = true;
            setup_player_pn.SetActive(false);
        }
    }

    public void setPlayerType(string type)
    {
        player.upgrades = type;
        updatePlayScreen();
    }

    public void resetPlayer()
    {
        player = null;
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void changeVolume()
    {
        volume = volume_sd.value;
    }

    public void changeControls(int i)
    {
        string old_val = controls[i];
        string new_val = control_ip_list[i].text;
        for(int k = 0;k<controls.Length;k++)
        {
            if(new_val.Equals(controls[k]) && k != i)
            {
                control_ip_list[i].text = old_val;
                return;
            }
        }
        if(new_val.Trim().Length > 0)
        {
            if(new_val.Trim().Length>1)
            {
                if(new_val.Trim().Length > 2)
                {
                    control_ip_list[i].text = old_val;
                }
                else
                {
                    if(new_val[0] == 'M' && (new_val[1] == '1' || new_val[1] == '2'))
                    {
                        control_ip_list[i].text = new_val;
                        controls[i] = new_val;
                    }
                    else
                    {
                        control_ip_list[i].text = old_val;
                    }
                }
            }
            else
            {
                control_ip_list[i].text = new_val;
                controls[i] = new_val;
            }
        }
        else
        {
            control_ip_list[i].text = old_val;
        }
    }

}
