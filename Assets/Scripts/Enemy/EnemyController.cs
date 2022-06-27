using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // [HideInInspector]
    // public bool mustPatrol;
    //// private bool mustTurn;

    // public float walkSpeed;
    // public Rigidbody2D rb;
    // //public Transform groundCheckPos;
     public LayerMask groundLayer;
    // public Collider2D bodyCollider;

    // private float xA;
    // private void Start()
    // {
    //     mustPatrol = true;
    // }
    // private void Update()
    // {
    //     if (mustPatrol)
    //     {
    //         Patrol();
    //     }


    // }

    // private void FixedUpdate()
    // {

    //     if (bodyCollider.IsTouchingLayers(groundLayer))
    //     {
    //         //mustTurn = true;
    //         Flip();
    //         Debug.Log("touching");
    //     }

    // }

    // void Patrol()
    // {

    //     //if (mustTurn)
    //     //{
    //     //    Flip();
    //     //    mustTurn = false;
    //     //}

    //     rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);

    // }

    // void Flip()
    // {

    //     // mustPatrol = false;
    //     xA = transform.localScale.x * -1;
    //     transform.localScale = new Vector2(xA, transform.localScale.y);
    //    // walkSpeed *= 1;
    //    //mustPatrol = true;
    //   }

    private float dirX;
    private float moveSpeed;
    private Rigidbody2D rb;
    private bool facingRight = false;
    private Vector3 localScale;

    private void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
        moveSpeed = 3f;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bricks")|| collision.CompareTag("Indestructibles"))
        {
            dirX *= -1f;
        }


    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }
    void CheckWhereToFace()
    {
        if (dirX > 0)
        {
            facingRight = true;
        }
        else if (dirX < 0)
        {
            facingRight = false;
        }

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;


    }
}
