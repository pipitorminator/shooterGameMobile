using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    public float sinkingSpeed = 2.5f;
    private bool isSinking;

    private ParticleSystem hitParticles;
    private Animator anim;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        hitParticles = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(Vector3.down * (sinkingSpeed * Time.deltaTime));
        }
    }

    protected override void Spawn()
    {
        currentHealth = startingHealth;
    }

    protected override void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
    }

    public void StartSinking()
    {
        navMeshAgent.enabled = false;
        rb.isKinematic = true;
        isSinking = true;

        Destroy(gameObject, 2);
    }

    public override void TakeDamage(int damage, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;

        hitParticles.transform.position = hitPoint;

        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }
}