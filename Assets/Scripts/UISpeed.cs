using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpeed : MonoBehaviour
{
   // private float liveValue;
    Text liveUI;
    PlayerController PlayerController;
    //FloatSO floatSO;

    // Start is called before the first frame update
    void Start()
    {
        liveUI = GetComponent<Text>();
        PlayerController= FindObjectOfType<PlayerController>();
        //  Debug.Log("live value start : " + liveValue);
    }

    // Update is called once per frame
    void Update()
    {
        // liveValue = PlayerPrefs.GetFloat("speed");
        liveUI.text = ": " + PlayerController.moveSpeed;
        //liveUI.text = ": " + floatSO.MoveSpeed;

    }
}
