using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    PlayerController playerController;
    BombController bombController;
    static bool init = false;

    public bool nextGame = false;

    public static bool Init { get => init; set => init = value; }

    void Awake()
    {
        // Prevent the GameObject from being destroyed on scene changes
        if (!GameManager.Init)
        {
            init = true;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        bombController = FindObjectOfType<BombController>();
    }
    public void CheckGameState()
    {
        PlayerPrefs.DeleteAll();

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //PlayerPrefs.SetInt("live", playerController.liveValue);
            //PlayerPrefs.SetFloat("speed", playerController.moveSpeed);
            //PlayerPrefs.SetInt("radius", bombController.explosionRadius);
            //PlayerPrefs.SetInt("bomb", bombController.bombAmount);

            Invoke(nameof(NextRound), 0.5f);
        }

    }

    public void FinalExit()
    {
        Invoke(nameof(NextRound), 0.5f);
    }

    public void PlayerDeath()
    {
        Invoke(nameof(NewRound), 2f);
    }


    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextRound()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            nextGame = true;
            
        }
        else
        {
            nextGame = false;

        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }



}
