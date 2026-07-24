using UnityEngine;

public class VampireAbility : AbilityFunction
{
    public void OnPower()
    {
        projectile = Instantiate(GetComponent<PlayerController>().vampireProjectile, transform);
        FinishPowerSetup();
    }
}
