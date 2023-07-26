using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : Pickup
{
    public override void PickMeUp()
    {
        Invertory.currentCoins++;
        UImanager.UpdateCoins();
        gameObject.SetActive(false);
    }
}
