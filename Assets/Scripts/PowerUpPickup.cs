using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [SerializeField]
    MonoScript powerUpScript;
    [SerializeField]
    GameObject flashingText;
    [SerializeField]
    string powerupName;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.TryGetComponent<PlayerController>(out PlayerController player))
        {
            if (collision.transform.TryGetComponent<AbilityFunction>(out AbilityFunction ability))
            {
                Destroy(player.gameObject.GetComponent<AbilityFunction>());
            }
                
            player.glowEyes.SetActive(true);

            player.gameObject.AddComponent(powerUpScript.GetClass());
            player.UpdateCountdown(5);
            
            GameObject flashingObject = Instantiate(flashingText, transform.position, transform.rotation);
            flashingObject.transform.GetChild(0).GetComponent<TMP_Text>().text = powerupName;

            Destroy(gameObject);
        }
        else if(collision.name.Contains("Powerup"))
        {
            Destroy(gameObject);
        }

    }

}
