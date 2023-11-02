using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TeleportDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject destination;
    private Transform tran;

    

    void Start()
    {
        tran = destination.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Teleport!!!");
       
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = new Vector2(tran.position.x + 1, tran.position.y);
        }
    }



 
}
