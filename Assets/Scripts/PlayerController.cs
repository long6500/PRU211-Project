using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Xml.Linq;
using UnityEngine.UIElements;

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
    [SerializeField] private FloatSO floatSO;


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
        //manager = FindObjectOfType<GameManager>();

        //Debug.Log(manager.nextGame ? "trueAwake" : "falseAwake");

        //manager = FindObjectOfType<GameManager>();

        //if (!manager.nextGame)
        //{
        //    liveValue = 2;
        //    moveSpeed = 10;
        //}
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Game"))
        {
            liveValue = 2;
            moveSpeed = 3;

        }
        else if (SceneManager.GetActiveScene().name.Equals("Game 1"))
        {
            manager = FindObjectOfType<GameManager>();

            //chon game scene tu menu
            if (!manager.nextGame)
            {
                liveValue = 2;
                moveSpeed = 10;
            }
            else
            {
                //liveValue = PlayerPrefs.GetInt("live");
                //moveSpeed = Mathf.RoundToInt(PlayerPrefs.GetFloat("speed") * 2);
                Debug.Log(floatSO.Live);
                Debug.Log(Mathf.RoundToInt(floatSO.MoveSpeed * 2));

                liveValue = floatSO.Live;
                moveSpeed = Mathf.RoundToInt(floatSO.MoveSpeed*2);
            }


        }
        else if (SceneManager.GetActiveScene().name.Equals("Game 2"))
        {
            liveValue = 2;
            moveSpeed = 3;
        }

        //PlayerPrefs.SetInt("live", liveValue);
        //PlayerPrefs.SetFloat("speed", moveSpeed);

    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            floatSO.Live = liveValue;
            floatSO.MoveSpeed = moveSpeed;
        }


        animator.SetFloat("Horizontal", movementJoystick.joystickVec.x);
        animator.SetFloat("Vertical", movementJoystick.joystickVec.y);
        animator.SetFloat("Speed", movementJoystick.joystickVec.sqrMagnitude);
        //manager = GetComponent<GameManager>();
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion") || other.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            liveValue--;
            //floatSO.Live--;
            flashEffect.Flash();
            AudioManager.Play(AudioName.PlayerHit);

            // DeathSequence();
        }

        if (other.CompareTag("Enemy"))
        {
            liveValue--;
            //floatSO.Live--;
            flashEffect.Flash();
            AudioManager.Play(AudioName.PlayerHit);

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
