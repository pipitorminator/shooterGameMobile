using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public int damage = 10;

    private Damager damager;

    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        damager = GetComponent<Damager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            damager.DoDamage(health, damage);
        }

        Destroy(gameObject);
    }
}