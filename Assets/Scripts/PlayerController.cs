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
    private void Update()
    {
       // movement.x = movementJoystick.joystickVec.x;
       // movement.y = movementJoystick.joystickVec.y;
        animator.SetFloat("Horizontal", movementJoystick.joystickVec.x);
        animator.SetFloat("Vertical", movementJoystick.joystickVec.y);
        animator.SetFloat("Speed", movementJoystick.joystickVec.sqrMagnitude);
       // Debug.Log("phone: " + movementJoystick.joystickVec.x);
       // Debug.Log("keyboard: " + movement.x);
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


}