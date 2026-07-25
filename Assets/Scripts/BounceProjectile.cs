using System.Collections;
using UnityEngine;

public class BounceProjectile : MonoBehaviour
{
    [SerializeField]
    GameObject impulseWave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EndBounceProjectile());
    }

    IEnumerator EndBounceProjectile()
    {
        yield return new WaitForSeconds(5);
        GameObject impulseObject = Instantiate(impulseWave, transform.position, transform.rotation);
        impulseObject.layer = gameObject.layer;
        Destroy(gameObject);
    }

}
