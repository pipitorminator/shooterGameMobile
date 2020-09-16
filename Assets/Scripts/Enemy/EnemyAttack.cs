using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject damageImpact;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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