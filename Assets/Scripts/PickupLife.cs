using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLife : Pickup
{
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFire>().AddLife();
        gameObject.SetActive(false);

    }
}
