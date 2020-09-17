using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRate = 1;
    public int damage = 10;

    private GameObject player;
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;
    private bool playerInRange;
    private float timer;

    public GameObject damageImpact;

    // Start is called before the first frame update
    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();

        player = FindObjectOfType<PlayerMovement>().gameObject;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > attackRate && playerInRange)
        {
            Attack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Attack()
    {
        timer = 0;
        Debug.Log(enemyHealth.currentHealth);
        if (enemyHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(damage, Vector3.zero);
        }
    }

    public void StartAttack()
    {
        damageImpact.SetActive(true);
    }

    public void FinishAttack()
    {
        damageImpact.SetActive(false);
    }
}