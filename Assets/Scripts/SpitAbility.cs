using UnityEngine;

public class SpitAbility : AbilityFunction
{


    public void OnPower()
    {
        AudioManager.instance.PlaySound(1);
        projectile = Instantiate(GetComponent<PlayerController>().spitProjectile, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<PlayerController>().lastXInput, 0) * 1500, ForceMode2D.Force);
        FinishPowerSetup();
    }
}
