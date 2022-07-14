using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBrick : MonoBehaviour
{

    public float destroyTime = 1f;

    [Header("SpawnDoor")]
    public GameObject spawnableItems;


    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnDestroy()
    {
           Instantiate(spawnableItems, transform.position, Quaternion.identity);
    }
}
