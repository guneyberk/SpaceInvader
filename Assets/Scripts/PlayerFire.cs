using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    private const float maxX = 2.25f;
    private const float minX = -2.25f;

    //private float speed =3f;


    private bool isShooting;

    //private float cooldown = 0.5f;

    [SerializeField] private ObjectPool objectPool = null;

    public ShipStats shipStats;
    private Vector2 offScreenPos = new Vector2 (0, -20f);
    private Vector2 startPos = new Vector2 (0, -4f);
    private float dirx;

    private void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLives = shipStats.maxLives;
        transform.position = startPos;
        UImanager.UpdateHealthbar(shipStats.currentHealth);
        UImanager.UpdateLives(shipStats.currentLives);
    }
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKey(KeyCode.A) && transform.position.x > minX)
        {
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        }
        if(Input.GetKey(KeyCode.D) && transform.position.x < maxX)
        {
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
#endif
        dirx = Input.acceleration.x;
        if(dirx <= -0.1f && transform.position.x > minX) 
        {
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        }
        if(dirx >= 0.1f && transform.position.x < maxX) 
        {
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
        }
    }

    public void ShootButton()
    {
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }

    }

    public void AddHealth()
    {
        if(shipStats.currentHealth == shipStats.maxHealth)
        {
            UImanager.UpdateScore(250);
        }
        else
        {
            shipStats.currentHealth++;
            UImanager.UpdateHealthbar(shipStats.currentHealth);
        }
    }
    public void AddLife()
    {
        if (shipStats.currentLives == shipStats.maxLives)
        {
            UImanager.UpdateScore(1000);
        }
        else
        {
            shipStats.currentLives++;
            UImanager.UpdateHealthbar(shipStats.currentLives);
        }
    }
    private IEnumerator Shoot()
    {
        isShooting = true;
        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject obj = objectPool.GetPooledObject();
        obj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(shipStats.fireRate);
        isShooting = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            collision.gameObject.SetActive(false);
            TakeDamage();
        }
    }

    private IEnumerator Respawn()
    {
        transform.position = offScreenPos;
        yield return new WaitForSeconds(2);
        shipStats.currentHealth = shipStats.maxHealth;
        transform.position = startPos;
        UImanager.UpdateHealthbar(shipStats.currentHealth);
    }

    public void TakeDamage()
    {
        shipStats.currentHealth--;
        UImanager.UpdateHealthbar(shipStats.currentHealth);

        if(shipStats.currentHealth <= 0)
        {
            shipStats.currentLives--;
            UImanager.UpdateLives(shipStats.currentLives);
            if (shipStats.currentLives <= 0)
            {
                Debug.Log("Game Over");
            }
            else
            {
                //Debug.Log("Respawn");
                 StartCoroutine(Respawn());
            }
        }
        
    }


}
