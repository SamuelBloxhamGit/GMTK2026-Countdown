using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public GameObject sprite;

    [SerializeField]
    public GameObject dashBump;

    [SerializeField]
    public GameObject bounceProjectile;

    [SerializeField]
    public GameObject vampireProjectile;

    [SerializeField]
    public GameObject guanoProjectile;
    [SerializeField] Rigidbody2D rb;

    [SerializeField]
    TMP_Text countdownText;

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
    }

    IEnumerator DecrementCountdown()
    {

        countdownText.text = countdown.ToString();

        while (countdown > 0)
        {
            yield return new WaitForSeconds(1);
            UpdateCountdown(-1);

            if(inHarm)
            {
                UpdateCountdown(-2);
            }

        }

        LoseGame();
    }
    
    void LoseGame()
    {

    }

    public void UpdateCountdown(int difference)
    {
        countdown = Mathf.Clamp(countdown + difference, 0,60);
        countdownText.text = countdown.ToString();
    }


    float dashSpeed = 900;
    float bumpAmount = 2000;
    public float dashCooldown = 0;

    public void OnDash()
    {
        if(dashCooldown <= 0)
        {
            dashBump.SetActive(true);
            rb.AddForce(moveInput * dashSpeed, ForceMode2D.Force);
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

    public void FixedUpdate()
    {
        if(controlsActive) rb.AddForce(moveInput * moveSpeed, ForceMode2D.Force);

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
            rb.AddForce(collision.GetComponentInParent<PlayerController>().moveInput * bumpAmount, ForceMode2D.Force);
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
            UpdateCountdown(-5);
        }
    }

    

}
