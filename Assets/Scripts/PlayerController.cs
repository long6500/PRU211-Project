using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed;
    public Animator animator;
    public MovementJoystick movementJoystick;
    GameManager manager;


    // [Header("Lives")]
    //public UIplayer uip;
    // [SerializeField] Text liveText;

    [SerializeField] private SimpleFlash flashEffect;

    public int liveValue;
    //public int LiveValue
    //{
    //    get { return liveValue; }
    //    set { liveValue = 2; }
    //}
    //[Header("Sprites")]
    //public AnimatedSpriteRenderer scpriteRendererDeath;


    private void Awake()
    {

    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Game"))
        {
            liveValue = 2;
            moveSpeed = 5;
        }
        else
        {
            liveValue = PlayerPrefs.GetInt("live");
            moveSpeed = PlayerPrefs.GetFloat("speed");
        }

        //PlayerPrefs.SetInt("live", liveValue);
        //PlayerPrefs.SetFloat("speed", moveSpeed);

    }
    private void Update()
    {

        animator.SetFloat("Horizontal", movementJoystick.joystickVec.x);
        animator.SetFloat("Vertical", movementJoystick.joystickVec.y);
        animator.SetFloat("Speed", movementJoystick.joystickVec.sqrMagnitude);
        manager = GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        //uip = GetComponent<UIplayer>();
        //liveValue = PlayerPrefs.GetInt("live");
        //moveSpeed = PlayerPrefs.GetFloat("speed");

     //  Debug.Log("lives :" + liveValue);



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
            liveValue--;
            // PlayerPrefs.SetInt("live", liveValue);

            flashEffect.Flash();
            // DeathSequence();
        }

        if (other.CompareTag("Enemy"))
        {
            liveValue--;
            //   PlayerPrefs.SetInt("live", liveValue);

            flashEffect.Flash();

            // DeathSequence();
        }

        // Debug.Log("live: " + liveValue);
        if (liveValue == 0)
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
