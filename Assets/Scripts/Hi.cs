using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hi : MonoBehaviour
{
    public TMP_Text Text;
    void Start()
    {
        if(PlayerPrefs.HasKey("Player Name"))
        {
            Text.text = "Hi, "+PlayerPrefs.GetString("Player Name");
        }
        else
            Text.text="Hi, noname";
    }

}
