using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRigid : MonoBehaviour
{
    Rigidbody rb;
    public float angle;
    private Transform player;
    float y;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMove>().transform;
        y = player.localEulerAngles.y;
    }

    void Update()
    {
        angle = Mathf.Atan2(rb.velocity.y, rb.velocity.z) * Mathf.Rad2Deg;
        rb.transform.localEulerAngles = new Vector3(-angle, y, 0);
    }
}
