using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore
{
    int[] Score = new int[11];
    string[] Name = new string[11];
    public int GetScore(int index)
    {
        return Score[index];
    }
    public string GetName(int index)
    {
        return Name[index];
    }

    public void SetScore(int index, int score)
    {
        Score[index] = score;
    }
    public void SetName(int index, string name)
    {
        Name[index] = name;
    }

    public void Sort()
    {
        int temp_num = 0;
        string temp_name;

        for (int write = 0; write < Score.Length; write++)
        {
            for (int sort = 0; sort < Score.Length - 1; sort++)
            {
                if (Score[sort] < Score[sort + 1])
                {
                    temp_num = Score[sort + 1];
                    Score[sort + 1] = Score[sort];
                    Score[sort] = temp_num;

                    temp_name = Name[sort + 1];
                    Name[sort + 1] = Name[sort];
                    Name[sort] = temp_name;
                }
            }
        }
    }
    public string Show()
    {
        string Text = "";
        for (int i = 0; i < Score.Length-1; ++i)
        {
            Text += (i + 1).ToString() + "\t" + Name[i] + "\t" + Score[i] + "\n";
        }
        return Text;
    }

    public void Save()
    {
        for(int i=0;i<11;++i)
        {
            PlayerPrefs.SetInt("Score" + i.ToString(), Score[i]);
            PlayerPrefs.SetString("Name" + i.ToString(), Name[i]);
        }
    }
    public void Load()
    {
        for(int i=0;i<11;++i)
        {
            SetName(i, PlayerPrefs.GetString("Name" + i.ToString(), "Empty"));
            SetScore(i, PlayerPrefs.GetInt("Score" + i.ToString(), 0));
        }
    }
}

public class Highscores : MonoBehaviour
{
    public Highscore highscore= new Highscore();
    public Text TextField;
    void Start()
    {
        if(PlayerPrefs.GetInt("NewHighscore")==1)
        {
            PlayerPrefs.SetInt("NewHighscore", 0);
            
            int score=PlayerPrefs.GetInt("Count");
            PlayerPrefs.SetInt("Count", 0);
            string name= PlayerPrefs.GetString("Player Name");
            bool IsInserted = false;
            highscore.Load();
            for (int i=0;i<10;++i)
            {
                if (highscore.GetScore(i)>0)
                {
                    continue;
                }
                else
                {
                    IsInserted = true;
                    highscore.SetName(i, name);
                    highscore.SetScore(i, score);
                    break;
                }
            }
            if(!IsInserted)
            {
                highscore.SetName(10, name);
                highscore.SetScore(10, score);
            }
            highscore.Sort();
            TextField.text = highscore.Show();
            highscore.Save();
        }
        else
        {
            highscore.Load();
            TextField.text = highscore.Show();
        }
    }
    void Update()
    {
        
    }
}


