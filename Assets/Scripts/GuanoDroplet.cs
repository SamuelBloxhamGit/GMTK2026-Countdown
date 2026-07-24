using System.Collections;
using UnityEngine;

public class GuanoDroplet : MonoBehaviour
{
    bool stuckPlayer = false;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(stuckPlayer) return;

        if(collision.transform.TryGetComponent<PlayerController>(out PlayerController player))
        {
            StartCoroutine(GuanoFreeze(player));
        }
        
    }

    IEnumerator GuanoFreeze(PlayerController player)
    {
        stuckPlayer = true;
        gameObject.layer = 0;

        player.GetComponent<Rigidbody2D>().simulated = false;
       // player.GetComponent<CircleCollider2D>().enabled = false;
        //player.controlsActive = false;
        player.transform.parent = transform;
        player.transform.position = transform.position;

        yield return new WaitForSeconds(7f);


        player.GetComponent<Rigidbody2D>().simulated = true;
        //player.GetComponent<CircleCollider2D>().enabled = true;
        //player.controlsActive = true;
        player.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DashBump")
        {
            GetComponent<Rigidbody2D>().AddForce(collision.GetComponentInParent<PlayerController>().moveInput * 2000, ForceMode2D.Force);
        }
    }

}
