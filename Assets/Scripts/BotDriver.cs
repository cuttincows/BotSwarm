using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotDriver : MonoBehaviour, IDamagable
{
    private NavMeshAgent navAgent;
    public GameObject target;

    public float MinDist = 0.5f;
    public float Health { get; private set; }
    public float MaxHealth = 2f;


    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        Health = MaxHealth;
        if (target == null)
        {
            target = FindAnyObjectByType<PlayerInputDriver>().gameObject;
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= MinDist)
        {
            return;
        }
        navAgent.destination = target.transform.position;    
    }

    public void Damage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
