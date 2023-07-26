using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayerFire : MonoBehaviour
{
    private void Update()
    {
        if(transform.position.y > 7f)
        {
            gameObject.SetActive(false);    
        }
    }
}
