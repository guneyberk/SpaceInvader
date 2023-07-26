using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Sprite[] states;
    private int health;
    private SpriteRenderer _sr;
    void Start()
    {
        health = 3;
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("FriendlyBullet"))
        {
            collision.gameObject.SetActive(false);
            health--;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
              _sr.sprite = states[health - 1];
            }
        }
    }
}
