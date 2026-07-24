using UnityEngine;

public class AbilityFunction : MonoBehaviour
{
    [HideInInspector]
    public GameObject projectile;

    public void FinishPowerSetup()
    {
        projectile.layer = gameObject.layer;
        Destroy(this);
    }


}
