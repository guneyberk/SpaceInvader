using UnityEngine;
[System.Serializable]
public class ShipStats
{
    [Range(1f, 5f)]
    public int maxHealth;
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int maxLives = 3;
    [HideInInspector]
    public int currentLives=3;

    public float shipSpeed;
    public float fireRate;

}
