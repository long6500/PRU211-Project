using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 5f;
    public Animator animator;
    public MovementJoystick movementJoystick;
    GameManager manager;

    
    //[Header("Sprites")]
    //public AnimatedSpriteRenderer scpriteRendererDeath;

    private void Update()
    {

        animator.SetFloat("Horizontal", movementJoystick.joystickVec.x);
        animator.SetFloat("Vertical", movementJoystick.joystickVec.y);
        animator.SetFloat("Speed", movementJoystick.joystickVec.sqrMagnitude);
        manager = GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (movementJoystick.joystickVec.y != 0)
        {
            // rb.velocity = new Vector2(movementJoystick.joystickVec.x * moveSpeed, movementJoystick.joystickVec.y * moveSpeed);
            rb.MovePosition(rb.position + movementJoystick.joystickVec * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }

        if (other.CompareTag("Enemy"))
        {
            DeathSequence();
        }
    }


    void DeathSequence()
    {
        // enabled = false;
        // GetComponent<BombController>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");

       // scpriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
      
        FindObjectOfType<GameManager>().PlayerDeath();
    }


}
