using System.Collections;
using UnityEngine;

public class SpitDroplet : MonoBehaviour
{

    [SerializeField]
    LineRenderer line;

    private void Awake()
    {
        line.SetPosition(0, transform.position);
    }

    private void Update()
    {
        line.SetPosition(1, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.rb.AddForce(Random.insideUnitCircle.normalized * 4000, ForceMode2D.Force);

            GameManager.instance.HitStop(1f);
            Destroy(gameObject);
        }        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "DashBump")
    //    {
    //        GetComponent<Rigidbody2D>().AddForce(collision.GetComponentInParent<PlayerController>().moveInput * 2000, ForceMode2D.Force);
    //    }
    //}

}
