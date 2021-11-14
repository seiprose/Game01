using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    NavMeshAgent nav;
    Transform target;
    Rigidbody enemyRb;
    [SerializeField] Image hpBar;
    [SerializeField] GameObject posion;

    private float enemySpeed = 5f;
    private float chaseRange = 20f;
    public float targetDistance;
    public float currentTime;
    private float attackTime = 1f;
    private float enemy_hp = 20f;
    private float enemyFullHp = 20f;
    
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
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
            ItemDrop();
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
                enemyRb.AddForce(transform.forward * 100f, ForceMode.Impulse);
                currentTime = 0;
            }
        }
    }

    void Damage(float damage)
    {
        enemy_hp -= damage;
        chaseRange = Mathf.Infinity;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Arrow")
        {
            Arrow arrow = other.gameObject.GetComponent<Arrow>();
            Damage(arrow.arrowDamage);
        }
    }

    void ItemDrop()
    {
        int i = Random.Range(0,10);
        if(i > 0)
        {
            Instantiate(posion, transform.position, transform.rotation);
        }
    }
}
