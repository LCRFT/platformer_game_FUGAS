using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject panel;
    //public Button MenuButton;
    private bool MenuVisibility=false,IsPaused=false;

    public void ShowMenu()
    {
        if (!MenuVisibility)
        {
            panel.gameObject.SetActive(true);
            //MenuButton.onClick.AddListener(VisibilityOn);
            MenuVisibility = true;
            TimePause();
            return;
        }
        if (MenuVisibility)
        {
            panel.gameObject.SetActive(false);
            //MenuButton.onClick.AddListener(VisibilityOff);
            MenuVisibility = false;
            TimeResume();
            return;
        }

    }
    public void TimeResume()
    {
        Time.timeScale = 1;
        IsPaused = false;
    }
    public void TimePause()
    {
        Time.timeScale = 0;
        IsPaused = true;
    }


}
