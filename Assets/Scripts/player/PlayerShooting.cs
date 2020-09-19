using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public AudioClip shootSound;
    private AudioPlayer audioPlayer;

    private GameObject player;
    private PlayerHealth playerHealth;

    public float fireRate = 0.5f;
    public float effectDisplay = 0.1f;

    public GameObject playerBullet;

    public Rigidbody bombPrefab;
    public Vector2 bombImpulse;
    public Image bombImage;
    private Text bombImageText;
    public float bombCoolDown = 10;
    private float bombTimer;

    private float timer;
    private Light gunLight;
    private ParticleSystem gunEffect;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        playerHealth = player.GetComponent<PlayerHealth>();
        gunLight = GetComponent<Light>();
        gunEffect = GetComponent<ParticleSystem>();
        bombImageText = bombImage.GetComponentInChildren<Text>();
        audioPlayer = GetComponent<AudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        bombTimer += Time.deltaTime;

        bombImage.fillAmount = bombTimer / bombCoolDown;

        if (bombTimer < bombCoolDown)
        {
            EnableBombCooldownText();
        }
        else
        {
            DisableBombCooldownText();
        }

        if (timer > effectDisplay)
        {
            DisableEffects();
        }
    }

    void DisableEffects()
    {
        gunLight.enabled = false;
    }

    private void EnableBombCooldownText()
    {
        bombImageText.enabled = true;
        bombImageText.text = Math.Ceiling(bombCoolDown - bombTimer).ToString();
    }

    private void DisableBombCooldownText()
    {
        bombImageText.enabled = false;
    }


    public void bomb()
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }

        if (!(bombTimer >= bombCoolDown))
        {
            return;
        }

        bombTimer = 0;
        Rigidbody newBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        newBomb.AddForce(transform.forward * bombImpulse.x, ForceMode.Impulse);
        newBomb.AddForce(transform.up * bombImpulse.y, ForceMode.Impulse);
    }

    public void Shoot()
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }

        if (!(timer > fireRate))
        {
            return;
        }

        audioPlayer.PlaySound(shootSound);

        timer = 0;

        GameObject newBullet = Instantiate(playerBullet, transform.position, transform.rotation);
        gunLight.enabled = true;
        gunEffect.Stop();
        gunEffect.Play();
    }
}