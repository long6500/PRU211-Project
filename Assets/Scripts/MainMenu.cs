using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }


    public void ChooseLevel()
    {
        SceneManager.LoadScene("LevelSelect");

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }


    //Choose Level Scenes
    public void Level1()
    {
        SceneManager.LoadScene("Game");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Game 1");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Game 2");
    }

}
