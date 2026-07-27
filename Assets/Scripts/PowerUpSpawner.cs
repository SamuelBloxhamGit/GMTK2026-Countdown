using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] powerUps;

    public bool powerUpSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RandomPowerSpawn());
    }


    IEnumerator RandomPowerSpawn()
    {
        yield return new WaitForSeconds(Random.Range(0,45));

        if (powerUpSpawned == false)
        {
            powerUpSpawned = true;
            GameObject powerUpPickup = Instantiate(powerUps[Random.Range(0, powerUps.Length)], transform.position, transform.rotation);
            powerUpPickup.GetComponent<PowerUpPickup>().spawnerReference = this;
        }
        else
        {
            print("didn't spawn");
        }
            StartCoroutine(RandomPowerSpawn());
    }

}
