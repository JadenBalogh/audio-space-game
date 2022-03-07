using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private int maxHealth = 10;
    public int MaxHealth { get => maxHealth; }

    [SerializeField] private float deathDelay = 0.5f;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSensitivity = 1f;
    [SerializeField] private Projectile projPrefab;
    [SerializeField] private float projSpeed = 5f;
    [SerializeField] private float shootInterval = 0.5f;

    private int health;
    public int Health { get => health; }

    private float lookAngle = 0f;
    private Vector2 lookDir;
    public Vector2 LookDir { get => lookDir; }

    private new Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private WaitForSeconds shootWait;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shootWait = new WaitForSeconds(shootInterval);
        health = maxHealth;
    }

    private void Start()
    {
        StartCoroutine(ShootLoop());
    }

    private void Update()
    {
        if (health <= 0) return;

        lookAngle += GameManager.AudioSystem.AudioInput * rotationSensitivity;
        lookDir = new Vector2(Mathf.Cos(lookAngle), Mathf.Sin(lookAngle));
        rigidbody2D.velocity = lookDir * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage();
        }
    }

    private void FixedUpdate()
    {
        if (health <= 0) return;

        transform.rotation = Quaternion.FromToRotation(Vector2.up, lookDir);
    }

    public void TakeDamage()
    {
        health--;
        healthbar.UpdateDisplay(this);
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            spriteRenderer.enabled = false;
            StartCoroutine(DelayDeath());
        }
    }

    private IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(deathDelay);
        GameManager.EndGame();
    }

    private IEnumerator ShootLoop()
    {
        yield return shootWait;
        while (true)
        {
            if (health <= 0) yield break;
            Vector2 spawnPos = (Vector2)transform.position + lookDir;
            Projectile proj = Instantiate(projPrefab, spawnPos, transform.rotation);
            proj.Shoot(lookDir * projSpeed);
            yield return shootWait;
        }
    }
}
