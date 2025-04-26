using System;
using Game.Scripts;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private EnemyAI enemyAI;

    private MeshRenderer _meshRenderer;
    private StateMachine _stateMachine;

    private void Start()
    {
        enemyAI.Init();
        _meshRenderer = GetComponent<MeshRenderer>();
        _stateMachine = enemyAI.StateMachine;
    }

    public void TakeDamage(int amount)
    {
        // DamagePopUpGenerator.current.CreatePopUp(amount.ToString(), true);
        health -= amount;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _stateMachine.ChangeState(new DeadState());
        _meshRenderer.enabled = false;
        deathParticle.SetActive(true);
    }
}
