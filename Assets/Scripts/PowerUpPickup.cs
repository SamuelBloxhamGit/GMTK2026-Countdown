using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [SerializeField]
    MonoScript powerUpScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.gameObject.AddComponent(powerUpScript.GetClass());
            player.UpdateCountdown(5);
            Destroy(gameObject);
        }
        else if(collision.name.Contains("Powerup"))
        {
            Destroy(gameObject);
        }

    }

}
