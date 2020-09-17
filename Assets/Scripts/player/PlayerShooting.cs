using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float fireRate = 0.5f;
    public float effectDisplay = 0.1f;

    public GameObject playerBullet;

    private float timer;
    private Light gunLight;
    private ParticleSystem gunEffect;

    private void Awake()
    {
        gunLight = GetComponent<Light>();
        gunEffect = GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > effectDisplay)
        {
            DisableEffects();
        }
    }

    void DisableEffects()
    {
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        if (!(timer > fireRate))
        {
            return;
        }

        timer = 0;

        GameObject newBullet = Instantiate(playerBullet, transform.position, transform.rotation);
        gunLight.enabled = true;
        gunEffect.Stop();
        gunEffect.Play();
    }
}