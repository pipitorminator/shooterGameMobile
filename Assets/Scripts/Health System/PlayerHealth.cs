using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Slider healthSlider;
    public Image damageImage;
    public Color damageColor;
    public float flashSpeed = 2;

    private bool damaged;

    private Animator animator;

    private PlayerMovement playerMovement;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageImage.color = damageColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    protected override void Spawn()
    {
        currentHealth = startingHealth;
    }

    protected override void Death()
    {
        playerMovement.enabled = false;
        
        isDead = true;
        animator.SetTrigger("Die");
        LevelController.instance.GameOver();
    }

    public override void TakeDamage(int damage, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }

        damaged = true;
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Death();
        }
    }
}