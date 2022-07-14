using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class BombController : MonoBehaviour
{
    [Header("Bomb")]

    public GameObject bombPrefab;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;

    public Button yourButton;
    private GameObject playerObj = null;


    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public LayerMask bricksLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;


    [Header("Bricks")]
    public Tilemap bricksTiles;
    public Bricks bricksPrefab;

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

    private IEnumerator PlaceBomb()
    {

        Vector2 position = playerObj.transform.position;

        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);


        Debug.Log("x: " + position.x);
        Debug.Log("y: " + position.y);
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

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
}
