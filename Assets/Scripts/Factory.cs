using System;
using UnityEngine;

public class Factory : MonoBehaviour, IDamagable
{
    public float MaxHealth = 10f;
    public float Health { get; private set; }

    public BotDriver ToSpawn;
    public float SpawnCooldown = 10f;
    private float _curCooldown;

    public Transform SpawnPoint;

    public BuildProgressRenderer BuildProgressRenderer;

    private void Awake()
    {
        Health = MaxHealth;
        _curCooldown = SpawnCooldown;
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

    // Update is called once per frame
    void Update()
    {
        _curCooldown -= Time.deltaTime;
        if (_curCooldown <= 0)
        {
            Spawn();
            _curCooldown = SpawnCooldown;
        }
        BuildProgressRenderer.UpdateProgress(_curCooldown / SpawnCooldown);
    }

    private void Spawn()
    {
        Instantiate(ToSpawn, SpawnPoint.transform.position, Quaternion.identity);
    }
}
