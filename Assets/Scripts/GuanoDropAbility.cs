using UnityEngine;

public class GuanoDropAbility : AbilityFunction
{


    public void OnPower()
    {
        print("power");
        GameObject guano = Instantiate(GetComponent<PlayerController>().guanoProjectile, transform.position, transform.rotation);
        guano.layer = gameObject.layer;
        Destroy(this);
    }
}
