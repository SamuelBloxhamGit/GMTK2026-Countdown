using UnityEngine;

public class VampireAbility : AbilityFunction
{


    public void OnPower()
    {
        print("vamp");
        GameObject projectile = Instantiate(GetComponent<PlayerController>().vampireProjectile, transform);
        projectile.layer = gameObject.layer;
        Destroy(this);
    }
}
