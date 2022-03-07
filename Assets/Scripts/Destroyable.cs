using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destroyable : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;

    [SerializeField] private int score = 10;
    public int Score { get => score; }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
