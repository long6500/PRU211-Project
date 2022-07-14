using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

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
        Debug.Log("Enemy remain: " + GameObject.FindGameObjectsWithTag("Enemy").Length);
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Invoke(nameof(NewRound), 3f);
        }
       
    }

    public void PlayerDeath()
    {
        Invoke(nameof(NewRound), 2f);
    }


  private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
