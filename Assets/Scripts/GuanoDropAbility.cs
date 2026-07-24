using UnityEngine;

public class GuanoDropAbility : AbilityFunction
{


    public void OnPower()
    {
        projectile = Instantiate(GetComponent<PlayerController>().guanoProjectile, transform.position, transform.rotation);
        FinishPowerSetup();
    }
}
