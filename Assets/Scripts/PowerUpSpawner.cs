using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] powerUps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RandomPowerSpawn());
    }


    IEnumerator RandomPowerSpawn()
    {
        yield return new WaitForSeconds(Random.Range(0,60));
        Instantiate(powerUps[Random.Range(0,powerUps.Length)], transform.position, transform.rotation);
        StartCoroutine(RandomPowerSpawn());
    }

}
