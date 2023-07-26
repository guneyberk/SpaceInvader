using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : Pickup
{
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFire>().AddHealth();
        gameObject.SetActive(false);
    }
}
