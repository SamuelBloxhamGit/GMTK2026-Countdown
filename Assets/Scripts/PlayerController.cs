using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;

    [SerializeField]
    TMP_Text countdownText;

    Vector2 moveInput;

    public int playerID;


    int countdown = 20;

    private void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();

        if (input != null)
        {
            playerID = input.playerIndex;
        }

        StartCoroutine(DecrementCountdown());

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
        countdown = Mathf.Clamp(countdown + difference, 0,30);
        countdownText.text = countdown.ToString();
    }


    float dashSpeed = 1500;
    public float dashCooldown = 0;

    public void OnDash()
    {
        if(dashCooldown <= 0)
        {
            rb.AddForce(moveInput * dashSpeed, ForceMode2D.Force);
            StartCoroutine(ResetDashCooldown());
        }
    }

    IEnumerator ResetDashCooldown()
    {
        dashCooldown = 1;

        while(dashCooldown > 0)
        {
            dashCooldown-= Time.deltaTime;
            yield return null;
        }
        dashCooldown = 0;

    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public float moveSpeed = 1;

    bool inHarm = false;

    public void FixedUpdate()
    {
        rb.AddForce(moveInput * moveSpeed, ForceMode2D.Force);




        //rb.linearVelocity = moveInput * moveSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SlowHarm")
        {
            inHarm = true;
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
