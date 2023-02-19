using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameController : MonoBehaviour
{
    public GameObject hero_save_dd;
    public GameObject hero_name_ib;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initPlayer(bool load)
    {
        string dir = "..\\Royal Adventure\\saves.txt";
        if (load)
        {
            string hero_name = hero_save_dd.GetComponent<TMP_Dropdown>().options[hero_save_dd.GetComponent<TMP_Dropdown>().value].text;
            if(hero_name.Length>0)
            {
                string[] lines = File.ReadAllLines(dir);
                foreach(string line in lines)
                {
                    string[] info = line.Split(",");
                    if(info[0].Equals(hero_name))
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
                StreamWriter writer = File.AppendText(dir);
                string info = hero_name + "," + 1 + "," + 0 + ",";
                writer.WriteLine(info);
                writer.Close();
            }
        }
    }
}
