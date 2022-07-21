using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    PlayerController playerController;
    BombController bombController;



    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();    
        bombController = FindObjectOfType<BombController>();
    }
    public void CheckGameState()
    {
        //int aliveCount = 0;

        //foreach (GameObject player in players)
        //{
        //    if (player.activeSelf)
        //    {
        //        aliveCount++;
        //    }
        //}

        //if (aliveCount <= 1)
        //{
        //    Invoke(nameof(NewRound), 3f);
        //}

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            PlayerPrefs.SetInt("live", playerController.liveValue);
            PlayerPrefs.SetFloat("speed", playerController.moveSpeed);
            PlayerPrefs.SetInt("radius", bombController.explosionRadius);
            PlayerPrefs.SetInt("bomb", bombController.bombAmount);

            //Debug.Log("current lives: " + playerController.liveValue);
            //Debug.Log("current speed: " + playerController.moveSpeed);

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
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
