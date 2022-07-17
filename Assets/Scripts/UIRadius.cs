using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIRadius : MonoBehaviour
{
 
    Text liveUI;

    BombController bombController;

    // Start is called before the first frame update
    void Start()
    {
        liveUI = GetComponent<Text>();
        bombController= FindObjectOfType<BombController>();


        
    }

    // Update is called once per frame
    void Update()
    {
        
        liveUI.text = ": " + bombController.explosionRadius;
    }
}
