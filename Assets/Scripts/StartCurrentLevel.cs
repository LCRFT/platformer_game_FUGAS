using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCurrentLevel : MonoBehaviour
{
    // Start is called before the first frame update
 public void StartLevel()
    {
        if (PlayerPrefs.HasKey("LevelIndex"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelIndex"));
        }
    }
}
