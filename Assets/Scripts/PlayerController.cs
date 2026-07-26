using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public GameObject deathSprite;
    [SerializeField]
    public GameObject sprite;


    [SerializeField]
    public SpriteRenderer bodySprite;

    [SerializeField]
    public Sprite[] bodyVariants;

    [SerializeField]
    public GameObject dashBump;

    [SerializeField]
    public GameObject spitProjectile;
    [SerializeField]
    public GameObject bounceProjectile;

    [SerializeField]
    public GameObject vampireProjectile;

    [SerializeField]
    public GameObject glowEyes;
    [SerializeField]
    public GameObject guanoProjectile;
    [SerializeField] public Rigidbody2D rb;

    [SerializeField]
    TMP_Text countdownText;

    [SerializeField]
    AudioSource screech;

    public Vector2 moveInput;

    public int playerID;


    int countdown = 30;

    private void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();

        if (input != null)
        {
            playerID = input.playerIndex;
        }

        StartCoroutine(DecrementCountdown());

        gameObject.layer = playerID+10;

        transform.position = GameManager.instance.playerSpawns[playerID].position;

        GameManager.instance.alivePlayers.Add(this);
    }

    IEnumerator DecrementCountdown()
    {

        countdownText.text = countdown.ToString();

        while (countdown > 0)
        {
            yield return new WaitForSeconds(1);
            UpdateCountdown(-1);

            countdownText.color = Color.white;

            if(countdown < 6 && countdown > 0)
            {
                AudioManager.instance.PlaySound(2);
                countdownText.color = Color.red;
            }


            if(inHarm)
            {
                UpdateCountdown(-2);
                GameManager.instance.HitStop(0.5f);
            }

        }

        LoseGame();
    }
    
    void LoseGame()
    {
        AudioManager.instance.PlaySound(3);
        Instantiate(deathSprite, transform.position, Quaternion.identity);
        GameManager.instance.alivePlayers.Remove(this);
        GameManager.instance.CheckAlivePlayers();
        Destroy(gameObject);
    }

    

    public void UpdateCountdown(int difference)
    {
        countdown = Mathf.Clamp(countdown + difference, 0,60);
        countdownText.text = countdown.ToString();

        if (countdown >= 40)
        {
            bodySprite.sprite = bodyVariants[2];
        }
        else if (countdown < 40 && countdown > 15)
        {
            bodySprite.sprite = bodyVariants[1];
        }
        else if(countdown <= 15)
        {
            bodySprite.sprite = bodyVariants[0];
        }


    }


    float dashSpeed = 900;
    float bumpAmount = 1500;
    public float dashCooldown = 0;

    public void OnDash()
    {
        if(dashCooldown <= 0)
        {
            screech.Play();
            dashBump.SetActive(true);
            rb.AddForce(lastMoveInput * dashSpeed, ForceMode2D.Force);
            StartCoroutine(ResetDashCooldown());
        }
    }

    IEnumerator ResetDashCooldown()
    {
        dashCooldown = 1;

        while(dashCooldown > 0)
        {
            if(dashCooldown < 0.8f) dashBump.SetActive(false);
            dashCooldown -= Time.deltaTime;
            yield return null;
        }
        dashCooldown = 0;

    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    float moveSpeed = 30;

    bool inHarm = false;

    public bool controlsActive = true;

    public Vector2 lastMoveInput;
    public float lastXInput = 1f;

    public void FixedUpdate()
    {
        if (controlsActive)
        {
            rb.AddForce(moveInput * moveSpeed, ForceMode2D.Force);


            if (moveInput.sqrMagnitude > 0)
            {
                lastMoveInput = moveInput.normalized;
            }
            if (moveInput.x != 0)
            {
                lastXInput = Mathf.Sign(moveInput.x);
            }
        }

        sprite.transform.localScale = new Vector3(lastXInput, 1, 1);
        
        sprite.transform.rotation = Quaternion.Euler(0, 0, moveInput.x*-40);

        //rb.linearVelocity = moveInput * moveSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SlowHarm")
        {
            inHarm = true;
        }
        else if(collision.tag == "DashBump")
        {
            GameManager.instance.HitStop(0.5f);
            rb.AddForce(collision.GetComponentInParent<PlayerController>().lastMoveInput * bumpAmount, ForceMode2D.Force);
        }
        else if (collision.transform.tag == "Impulse")
        {
            GameManager.instance.HitStop(1f);
            rb.AddForce((collision.transform.position - transform.position).normalized * 2000, ForceMode2D.Force);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SlowHarm")
        {
            inHarm = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "InstantHarm")
        {
            GameManager.instance.HitStop(1);

            UpdateCountdown(-4);
        }
    }

    

}
