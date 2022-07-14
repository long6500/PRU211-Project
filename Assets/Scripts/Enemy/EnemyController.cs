using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float dirX;
    private float dirY;

    private bool moveLR = true;


    private float moveSpeed;
    private Rigidbody2D rb;
    private bool facingRight = false;
    private Vector3 localScale;
    public Animator anim;
    
    private void Start()
    {

        //  anim = GetComponent<Animator>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = 1f;
        dirY = 1f;
        moveSpeed = 3f;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("COLLIDE");
        if (collision.CompareTag("Bricks") || collision.CompareTag("Indestructibles") || collision.CompareTag("Bomb"))
        {
             dirX *= -1f;
   
        
        }


        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Die();

        }


    }

    private void Die()
    {

        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        
        Invoke(nameof(OnDeathSequenceEnded), (float)0.5);
        
    }

    //private void OnDeathSequenceEnded()
    //{
    //    //gameObject.SetActive(false);
        
    //    Destroy(gameObject);
       
    //}

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckGameState();
    }



    private void FixedUpdate()
    {
        Debug.Log("moveLR: " + moveLR);
        if (moveLR)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, 0);
           //Debug.Log("veloc: "+ rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, dirY * moveSpeed);
        }
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
