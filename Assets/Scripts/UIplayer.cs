using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIplayer : MonoBehaviour
{

 //   private int liveValue;
    Text liveUI;
    PlayerController playerController;
    

    // Start is called before the first frame update
    void Start()
    {
        liveUI = GetComponent<Text>();
      playerController = FindObjectOfType<PlayerController>();
    

      //  Debug.Log("live value start : " + liveValue);
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("live value update : " + liveValue);

      //  liveValue = PlayerPrefs.GetInt("live");
        liveUI.text = ": " + playerController.liveValue;
    }
}
