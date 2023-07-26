using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterPos : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletDeactivatePos;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y>bulletDeactivatePos || transform.position.y < -bulletDeactivatePos)
        {
            gameObject.SetActive(false);
        }


    }
}
