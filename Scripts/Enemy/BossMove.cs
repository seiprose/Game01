using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    NavMeshAgent nav;
    Rigidbody rb;
    Transform target;
    [SerializeField] Rigidbody ball;
    [SerializeField] Image hpBar;

    public float enemySpeed = 5f;
    public float chaseRange = 20f;
    public float targetDistance;
    public float currentTime;
    public float dizzyTime;
    public float enemy_hp = 100;
    
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<PlayerMove>().transform;
    }

    void Update()
    {
        hpBar.fillAmount = enemy_hp / 100; 
        targetDistance = Vector3.Distance(transform.position, target.position);
        if(targetDistance < chaseRange)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion faceRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, faceRot, Time.deltaTime * 2f);
            nav.enabled = true;
            nav.SetDestination(target.position);
            BallAttack();
        }
    }

    void BallAttack()
    {
        if(targetDistance < nav.stoppingDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= 5f)
            {
                Rigidbody newBall = (Rigidbody)Instantiate(ball, transform.position, transform.rotation);
                newBall.AddForce(-target.forward * 3f);
                currentTime = 0;
            }
        }
    }

    public void Damage(float damage)
    {
        enemy_hp -= damage;

        if(enemy_hp <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Arrow")
        {
            Arrow arrow = other.gameObject.GetComponent<Arrow>();
            Damage(arrow.arrowDamage);
            if(arrow.debuff == "Dizzy")
            {
                Dizzy(true);
            }
        }
    }

    void Dizzy(bool dizzy)
    {
        if(dizzy)
        {
            nav.enabled = false;
            dizzyTime += Time.deltaTime;
            if(dizzyTime >= 5f)
            {
                dizzy = false;
                dizzyTime = 0;
            }
        }
    }
}
