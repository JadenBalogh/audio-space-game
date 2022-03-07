using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Destroyable>(out Destroyable dest))
        {
            dest.Die();
            GameManager.ScoreSystem.AddScore(dest.Score);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Shoot(Vector2 velocity)
    {
        rigidbody2D.velocity = velocity;
    }
}
