using UnityEngine;

public class AbilityFunction : MonoBehaviour
{
    [HideInInspector]
    public GameObject projectile;

    public void FinishPowerSetup()
    {
        projectile.layer = gameObject.layer;

        gameObject.GetComponent<PlayerController>().glowEyes.SetActive(false);
        Destroy(this);
    }


    public void FinishPowerSetupCutomLayer(int layer)
    {
        projectile.layer = layer;

        gameObject.GetComponent<PlayerController>().glowEyes.SetActive(false);
        Destroy(this);
    }

}
