using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destroyable
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotSpeed = 1f;
    [SerializeField] private float destroyDist = 15f;

    private float rotAngle;
    private Vector2 moveDir;
    private new Rigidbody2D rigidbody2D;

    private Transform player;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Vector2 currDir = rigidbody2D.velocity.normalized;
        // Vector2 dirToPlayer = (player.position - transform.position).normalized;
        // float rotDir = Mathf.Sign(Vector2.SignedAngle(currDir, dirToPlayer));
        // rotAngle += rotDir * rotSpeed;
        // moveDir = new Vector2(Mathf.Cos(rotAngle), Mathf.Sin(rotAngle));

        Vector2 dirToPlayer = (player.position - transform.position).normalized;
        moveDir = dirToPlayer;

        rigidbody2D.velocity = moveDir * moveSpeed;
        if ((transform.position - player.position).sqrMagnitude > destroyDist * destroyDist)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.FromToRotation(Vector2.up, moveDir);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage();
            Die();
        }
    }
}
