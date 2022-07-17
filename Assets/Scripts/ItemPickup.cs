using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
        Live,
    }

    public ItemType type;

  

  

    private void Awake()
    {
        

    }

    private void OnItemPickup(GameObject player)
    {

       // moveSpeed = PlayerPrefs.GetFloat("speed");
       //// liveValue = PlayerPrefs.GetInt("live");
       // explosionRadius = PlayerPrefs.GetInt("radius");
       // bombAmount = PlayerPrefs.GetInt("bomb");

        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;

            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius++;
                break;

            case ItemType.Live:
                player.GetComponent<PlayerController>().liveValue++;
               
                break;

            case ItemType.SpeedIncrease:
                //moveSpeed++;
                //PlayerPrefs.SetFloat("speed", moveSpeed);
                player.GetComponent<PlayerController>().moveSpeed++;

                break;

           
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }
        //Debug.Log("on trigger item");
        
    }
}
