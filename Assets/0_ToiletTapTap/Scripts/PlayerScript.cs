using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Audio Clips")] [SerializeField]
    private AudioClip FlyAudioClip;

    [SerializeField] private AudioClip DeathAudioClip;
    [SerializeField] private AudioClip ScoredAudioClip;
    [SerializeField] private AudioSource audioSource;

    [Header("Physics")] [SerializeField] private float velocityPerJump = 3;
    [SerializeField] private float xSpeed = 1;

    [Header("Others")] [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rig;

    [Header("Raise event")] [SerializeField]
    private BoolEventChannel onRestartEventChannel;

    [SerializeField] private BoolEventChannel onWelcomeEventChannel;

    private const string ActiveFly = "flypower";

    private void OnEnable()
    {
        onRestartEventChannel.RaiseEvent(false);
        onWelcomeEventChannel.RaiseEvent(true);

        GameObject[] pannels = GameObject.FindGameObjectsWithTag("Pannel");
        foreach (GameObject pannel in pannels)
        {
            Destroy(pannel);
        }
    }

    // Use this for initialization
    void Start()
    {
        anim.SetFloat(ActiveFly, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat(ActiveFly, rig.velocity.y);

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
            if (rig.velocity.y < -1)
            {
                var high = rig.mass * 5500 * Time.deltaTime;
                rig.AddForce(new Vector2(0, high));
            }
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
        rig.velocity = new Vector2(0, velocityPerJump);
        audioSource.PlayOneShot(FlyAudioClip);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag ==
                "pannelBlank") //Pannelblank is an empty gameobject with a collider between the two pipes
            {
                audioSource.PlayOneShot(ScoredAudioClip);
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
        audioSource.PlayOneShot(DeathAudioClip);
        anim.SetBool("dead", true);
        Debug.Log("Player Dies");
    }
}