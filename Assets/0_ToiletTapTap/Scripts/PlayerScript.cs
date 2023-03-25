using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerScript : MonoBehaviour
{
    [Header("Audio Clips")] [SerializeField]
    private AudioClip FlyAudioClip;

    [SerializeField] private AudioClip DeathAudioClip;
    [SerializeField] private AudioClip ScoredAudioClip;

    [Header("Physics")] [SerializeField] private float velocityPerJump = 3;
    [SerializeField] private float xSpeed = 1;

    [Header("Others")] [SerializeField] private Animator anim;

    [Header("Raise event")] [SerializeField]
    private BoolEventChannel onRestartEventChannel;

    [SerializeField] private BoolEventChannel onWelcomeEventChannel;

    private const string ActiveFly = "flypower";
    private Rigidbody2D rb;

    private void OnEnable()
    {
        // onRestartEventChannel.RaiseEvent(false);
        // onWelcomeEventChannel.RaiseEvent(true);
    }

    // Use this for initialization
    void Start()
    {
        anim.SetFloat(ActiveFly, 0.0f);
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat(ActiveFly, rb.velocity.y);

        if (GameStateManager.GameState == GameState.Intro)
        {
            MoveBirdOnXAxis();
            if (WasTouchedOrClicked())
            {
                BoostOnYAxis();
                GameStateManager.GameState = GameState.Playing;
                ScoreManagerScript.Score = 0;
                onWelcomeEventChannel.RaiseEvent(false);
                Debug.Log("Start Game");
            }
        }

        else if (GameStateManager.GameState == GameState.Playing)
        {
            MoveBirdOnXAxis();
            if (WasTouchedOrClicked())
            {
                onWelcomeEventChannel.RaiseEvent(false);
                BoostOnYAxis();
            }
        }
    }


    void FixedUpdate()
    {
        //just jump up and down on intro screen
        if (GameStateManager.GameState == GameState.Intro)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < -1) //when the speed drops, give a boost
                GetComponent<Rigidbody2D>()
                    .AddForce(new Vector2(0,
                        GetComponent<Rigidbody2D>().mass * 5500 * Time.deltaTime)); //lots of play and stop 
            //and play and stop etc to find this value, feel free to modify
        }
    }

    bool WasTouchedOrClicked()
    {
        if (Input.GetButtonUp("Jump") || Input.GetMouseButtonDown(0) ||
            (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended))
            return true;
        else
            return false;
    }

    void MoveBirdOnXAxis()
    {
        transform.position += new Vector3(Time.deltaTime * xSpeed, 0, 0);
    }

    void BoostOnYAxis()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocityPerJump);
        GetComponent<AudioSource>().PlayOneShot(FlyAudioClip);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag ==
                "pannelBlank") //Pannelblank is an empty gameobject with a collider between the two pipes
            {
                GetComponent<AudioSource>().PlayOneShot(ScoredAudioClip);
                ScoreManagerScript.Score++;
            }
            else if (col.gameObject.tag == "Pannel")
            {
                PlayerDies();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag == "Floor")
            {
                PlayerDies();
            }
        }
    }

    void PlayerDies()
    {
        onRestartEventChannel.RaiseEvent(true);
        GameStateManager.GameState = GameState.Dead;
        GetComponent<AudioSource>().PlayOneShot(DeathAudioClip);
        anim.SetBool("dead", true);
        Debug.Log("Player Dies");
    }
}