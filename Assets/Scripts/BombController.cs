using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public KeyCode inputKey = KeyCode.LeftShift;
    public GameObject bombPrefab;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;
    public Button yourButton;
    private GameObject playerObj = null;

    private void Start()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }

        Button btn = yourButton.GetComponent<Button>();
        Debug.Log("qweqwe");
        btn.onClick.AddListener(Wrapper);
    }
    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }


    public void Wrapper()
    {
        if (bombsRemaining > 0 )
        {
            Debug.Log("asdasd");
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {

        Vector2 position = playerObj.transform.position;

        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        Debug.Log("bombasdasd");
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        //Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        //explosion.SetActiveRenderer(explosion.start);
        //explosion.DestroyAfter(explosionDuration);

        //Explode(position, Vector2.up, explosionRadius);
        //Explode(position, Vector2.down, explosionRadius);
        //Explode(position, Vector2.left, explosionRadius);
        //Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb.gameObject);
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
