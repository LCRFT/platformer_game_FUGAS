using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public int SceneNumber;
    public void Load()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneNumber);
    }

}
