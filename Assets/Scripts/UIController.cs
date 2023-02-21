using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject[] screens;
    public GameObject hero_save_dd;
    int screen;
    // Start is called before the first frame update
    void Start()
    {
        screen = 0;
        changeScreen(screen);
        
        string dir = "..\\Royal Adventure\\saves.txt";
        if(File.Exists(dir))
        {
            List<TMP_Dropdown.OptionData> data = new List<TMP_Dropdown.OptionData>();
            string[] lines = File.ReadAllLines(dir);
            foreach(string line in lines)
            {
                string[] info = line.Split(",");
                TMP_Dropdown.OptionData op = new TMP_Dropdown.OptionData();
                op.text = info[0];
                data.Add(op);
            }
            hero_save_dd.GetComponent<TMP_Dropdown>().options = data;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScreen(int cur_screen)
    {
        for(int i = 0;i<screens.Length;i++)
        {
            if(i == cur_screen)
            {
                screens[i].SetActive(true);
            }
            else
            {
                screens[i].SetActive(false);
            }
        }
    }

}
