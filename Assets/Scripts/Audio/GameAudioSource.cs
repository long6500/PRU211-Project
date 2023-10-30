using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameAudioSource : MonoBehaviour
{

    private void Awake()
    {
        //Neu chua co thif khoi tao
        //make sure we only have 1 of this game object in the game
        if (!AudioManager.Initialized)
        {
            //initialize audio manager and persist audio source across scenes
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioManager.Initialize(audioSource);
            DontDestroyOnLoad(gameObject);

            Debug.Log("khoi tao Audio Source");
        }
        else
        {
            //destroy duplicate game object 
            Destroy(gameObject);
        }
    }

}
