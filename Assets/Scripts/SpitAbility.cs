using UnityEngine;

public class SpitAbility : AbilityFunction
{


    public void OnPower()
    {
        projectile = Instantiate(GetComponent<PlayerController>().spitProjectile, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<PlayerController>().lastXInput, 0) * 2000, ForceMode2D.Force);
        FinishPowerSetup();
    }
}
