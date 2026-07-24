using UnityEngine;

public class BounceAbility : AbilityFunction
{


    public void OnPower()
    {
        projectile = Instantiate(GetComponent<PlayerController>().bounceProjectile, transform.position, transform.rotation);
        FinishPowerSetup();
    }
}
