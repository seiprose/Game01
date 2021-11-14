using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoraAttack : MonoBehaviour
{
    Rigidbody rb;
    float currentTime;
    private bool destroyBall = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(destroyBall)
        {
            currentTime += Time.deltaTime;
            if(currentTime > 5f)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Player")
        {
            rb.useGravity = true;
            destroyBall = true;
        }
    }
}
