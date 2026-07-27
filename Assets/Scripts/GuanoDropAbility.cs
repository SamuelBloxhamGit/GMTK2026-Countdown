using UnityEngine;

public class GuanoDropAbility : AbilityFunction
{


    public void OnPower()
    {
        projectile = Instantiate(GetComponent<PlayerController>().guanoProjectile, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -200), ForceMode2D.Force);
        FinishPowerSetup();
    }
}
