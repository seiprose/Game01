using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BoraMove : MonoBehaviour
{
    NavMeshAgent nav;
    Transform target;
    [SerializeField] Image hpBar;
    [SerializeField] Rigidbody BoraAttack;
    [SerializeField] GameObject posion;
    private float enemySpeed = 5f;
    private float chaseRange = 30f;
    public float targetDistance;
    public float currentTime;
    private float attackTime = 3f;
    private float enemy_hp = 15f;
    private float enemyFullHp = 15f;
    
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerMove>().transform;
    }

    void Update()
    {   
        hpBar.fillAmount = enemy_hp / enemyFullHp; 
        targetDistance = Vector3.Distance(transform.position, target.position);
        if(targetDistance <= chaseRange)
        {
            AttackTarget();
        }

        if (enemy_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void AttackTarget()
    {
        nav.enabled = true;
        nav.speed = enemySpeed;
        nav.SetDestination(target.position);
        if (targetDistance <= nav.stoppingDistance)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion faceRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, faceRot, Time.deltaTime * 2f);
            currentTime += Time.deltaTime;
            if (currentTime >= attackTime)
            {
                Rigidbody newBoraAttack = (Rigidbody) Instantiate(BoraAttack, transform.position, transform.rotation);
                newBoraAttack.AddForce((newBoraAttack.transform.forward - target.forward)* 10f);
                currentTime = 0;
            }
        }
    }

    void Damage(float damage)
    {
        enemy_hp -= damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Arrow")
        {
            Arrow arrow = other.gameObject.GetComponent<Arrow>();
            Damage(arrow.arrowDamage);
        }
    }
}
