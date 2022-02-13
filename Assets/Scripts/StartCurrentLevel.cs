using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCurrentLevel : MonoBehaviour
{
 public void StartLevel()
    {
        if (PlayerPrefs.HasKey("LevelIndex"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelIndex"));
        }
    }
}
