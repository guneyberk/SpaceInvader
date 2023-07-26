using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ObjectPool objectPool = null;
    private Vector3 _hMoveDistacne = new Vector3(0.05f,0,0);
    private Vector3 _vMoveDistacne = new Vector3(0,0.15f,0);

    private const float MAX_LEFT = -2f;
    private const float MAX_RIGHT = 2.11f;

    private bool _movingRight;
    private float _moveTimer = 0.01f;
    private float _moveTime = 0.005f;

    private const float MAX_MOVE_SPEED = 0.02f;
    private float _shootTimer = 3f;
    private const float _shootTime = 3f;
    public static List<GameObject> allAliens = new List<GameObject>();

    public GameObject motherShipPrefab;
    private Vector2 motherShipSpawnPos = new Vector2(3,3.5f);
    private float motherShipTimer = 1f;

    private const float MOTHERSHIP_MIN = 15f;
    private const float MOTHERSHIP_MAX = 60f;
    private const float START_Y = 1.7f;
    private bool entering = true;


    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Alien"))
        {
            allAliens.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (entering)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10 );
            if(transform.position.y <= START_Y)
            {
                entering = false;
            }
        }
        else
        {
            if (_moveTimer <= 0)
            {
                MoveEnemies();
            }
            if (_shootTimer <= 0)
            {
                Shoot();
            }

            if (motherShipTimer <= 0)
            {
                SpawnMotherShip();
            }
            _shootTimer -= Time.deltaTime;
            _moveTimer -= Time.deltaTime;
            motherShipTimer -= Time.deltaTime;
        }
       



    }

    private void SpawnMotherShip()
    {
        Instantiate(motherShipPrefab,motherShipSpawnPos,Quaternion.identity);
        motherShipTimer =Random.Range(MOTHERSHIP_MIN,MOTHERSHIP_MAX);
    }

    private void MoveEnemies()
    {
        int hitMax = 0;
        if(allAliens.Count > 0)
        {

            for(int i = 0; i < allAliens.Count; i++)
            {
                if(_movingRight)
                {
                    allAliens[i].transform.position += _hMoveDistacne;
                }
                else
                {
                    allAliens[i].transform.position -= _hMoveDistacne;
                }
                if (allAliens[i].transform.position.x > MAX_RIGHT || allAliens[i].transform.position.x <MAX_LEFT)
                {
                    hitMax++;
                }
            }
            if (hitMax > 0)
            {
                for(int i = 0; i < allAliens.Count; i++)
                {
                    allAliens[i].transform.position -= _vMoveDistacne;

                }
                _movingRight = !_movingRight;
            }
            _moveTimer = GetMoveSpeed();
        }
    }


    private float GetMoveSpeed()
    {
        float f = allAliens.Count * _moveTime;
        
        if (f < MAX_MOVE_SPEED)
        {
            return MAX_MOVE_SPEED;
        }
        else
        {
            return f;   
        }
    }

    private void Shoot()
    {
        Vector2 pos = allAliens[Random.Range(0,allAliens.Count)].transform.position;
        // Instantiate(bulletPrefab, pos, Quaternion.identity);
        GameObject obj = objectPool.GetPooledObject();
        obj.transform.position = pos;
        _shootTimer = _shootTime;
    }
}
