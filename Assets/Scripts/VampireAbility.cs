using UnityEngine;

public class VampireAbility : AbilityFunction
{
    public void OnPower()
    {
        projectile = Instantiate(GetComponent<PlayerController>().vampireProjectile, transform);
        //projectile.layer = gameObject.layer;
        //projectile.tag = LayerMask.LayerToName(projectile.layer) + "vamp";

        projectile.tag = "vamp";


        FinishPowerSetupCutomLayer(gameObject.layer+10);
    }
}
