using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float fullCharge;
    public float arrowDamage;
    public string debuff;


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Env")
        {
            Destroy(gameObject);
        }
    }
}
