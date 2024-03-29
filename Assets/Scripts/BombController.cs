using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using System;

public class BombController : MonoBehaviour
{




    [Header("Bomb")]

    public GameObject bombPrefab;
    public float bombFuseTime = 3f;
    public int bombAmount;
    private int bombsRemaining;

    public Button yourButton;
    private GameObject playerObj = null;


    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    // public LayerMask bricksLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius;


    [Header("Bricks")]
    public Tilemap bricksTiles;
    public Bricks bricksPrefab;

    [Header("ExitBrick")]
    public Tilemap exitBrickTile;
    public ExitBrick exit;


    GameManager manager;
    [SerializeField] private FloatSO floatSO;



    public enum MidpointRounding { };

    private void Start()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }

        Button btn = yourButton.GetComponent<Button>();
        //  Debug.Log("qweqwe");
        btn.onClick.AddListener(Wrapper);

        if (SceneManager.GetActiveScene().name.Equals("Game"))
        {
            explosionRadius = 1;
            bombAmount = 1;
            bombPrefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        else if (SceneManager.GetActiveScene().name.Equals("Game 1"))
        {
            manager = FindObjectOfType<GameManager>();
            if (!manager.nextGame)
            {
                explosionRadius = 1;
                bombAmount = 1;
                bombsRemaining = bombAmount;
            }
            else
            {
                //explosionRadius = Mathf.RoundToInt(PlayerPrefs.GetInt("radius") / 2);
                explosionRadius = Mathf.RoundToInt(floatSO.Radius / 2);
                if (explosionRadius <= 1)
                {
                    explosionRadius = 1;
                }
                bombAmount = floatSO.Bomb;
                bombsRemaining = bombAmount;
            }


            bombPrefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else if (SceneManager.GetActiveScene().name.Equals("Game 2"))
        {
            explosionRadius = 1;
            bombAmount = 1;
            bombsRemaining = bombAmount;
            //  Debug.Log("reamain: " + bombsRemaining);

        }

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            floatSO.Bomb = bombAmount;
            floatSO.Radius = explosionRadius;
        }
    }


    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }


    public void Wrapper()
    {
        if (bombsRemaining > 0)
        {
            // Debug.Log("asdasd");
            StartCoroutine(PlaceBomb());
        }
    }
    AudioSource audioSource;

    private IEnumerator PlaceBomb()
    {

        Vector2 position = playerObj.transform.position;

        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);


        //  Debug.Log("x: " + position.x);
        //  Debug.Log("y: " + position.y);
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        // Debug.Log("bombasdasd");
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        AudioManager.Play(AudioName.BombExplode);

        Destroy(bomb.gameObject);
        bombsRemaining++;
    }


    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            ClearBricks(position);
            ClearExitBrick(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);


        Explode(position, direction, length - 1);


    }

    private void ClearBricks(Vector2 position)
    {
        Vector3Int cell = bricksTiles.WorldToCell(position);
        TileBase tile = bricksTiles.GetTile(cell);



        if (tile != null)
        {
            Instantiate(bricksPrefab, position, Quaternion.identity);
            bricksTiles.SetTile(cell, null);
        }
    }

    private void ClearExitBrick(Vector2 position)
    {
        Vector3Int cell = exitBrickTile.WorldToCell(position);
        TileBase tile = exitBrickTile.GetTile(cell);

        if (tile != null)
        {
            //create prefab
            Instantiate(exit, position, Quaternion.identity);
            //remove tilemap
            exitBrickTile.SetTile(cell, null);
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
        Debug.Log("bomb: " + bombAmount);
        Debug.Log("bomb remain : " + bombsRemaining);


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
}
