using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerUD : MonoBehaviour
{

   // public LayerMask groundLayer;

    private float dirY;
    private float moveSpeed;
    private Rigidbody2D rb;
    private bool facingUp = false;
    private Vector3 localScale;

    private void Start()
    {
        localScale = transform.GetChild(0).localScale;
        rb = GetComponent<Rigidbody2D>();
        dirY = 1f;
        moveSpeed = 3f;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bricks") || collision.CompareTag("Indestructibles"))
        {
            dirY *= -1f;
        }


    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, dirY * moveSpeed);
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }
    void CheckWhereToFace()
    {
        if (dirY > 0)
        {
            facingUp = true;
        }
        else if (dirY < 0)
        {
            facingUp = false;
        }

        if (((facingUp) && (localScale.y < 0)) || ((!facingUp) && (localScale.y > 0)))
            localScale.y *= -1;

        transform.GetChild(0).localScale = localScale;


    }
}
