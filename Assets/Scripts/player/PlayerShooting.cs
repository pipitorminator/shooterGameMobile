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
    public GameObject throwBombArea;
    public float bombTime = 1f;

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

    Vector3 CalculateVelocity(Vector3 origin, Vector3 target, float time)
    {
        //define distance x and y
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        //create a float that represent our distance
        float dY = distance.y;
        float dXZ = distanceXZ.magnitude;

        //Create our velocity on X and Y
        float velocityXZ = dXZ / time;
        float velocityY = dY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        //Calculate velocity on X and put velocity on Y
        Vector3 velocity = distanceXZ.normalized;
        velocity *= velocityXZ;
        velocity.y = velocityY;

        return velocity;
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

        Vector3 objectVelocity = CalculateVelocity(transform.position, throwBombArea.transform.position, bombTime);

        Rigidbody newBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        newBomb.velocity = objectVelocity;
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