using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private float currentTime;
  
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > 5f)
        {
            Destroy(gameObject);
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
