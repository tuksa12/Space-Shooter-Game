using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void LoadLevelByIndex(int levelIndex)
    {
        //Player.ResetStats();
        Player.score = 0;
        Player.lives = 3;
        Player.missed = 0;
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevelByName(string levelName)
    {
        //Player.ResetStats();
        Player.score = 0;
        Player.lives = 3;
        Player.missed = 0;
        SceneManager.LoadScene(levelName);
    }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            Player.score = 0;
            Player.lives = 3;
            Player.missed = 0;
            SceneManager.LoadScene("Level 1");
        }
    }
}