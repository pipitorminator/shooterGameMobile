using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public void DoDamage(Health health, int damage)
    {
        health.TakeDamage(damage, transform.position);
    }
}