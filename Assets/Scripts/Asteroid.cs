using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Destroyable
{
    [SerializeField] private float maxMoveSpeed = 1f;
    [SerializeField] private float maxRotSpeed = 4f;
    [SerializeField] private float destroyDist = 15f;

    private float rotAngle;
    private float rotSpeed;
    private new Rigidbody2D rigidbody2D;

    private Transform player;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody2D.velocity = Random.insideUnitCircle * maxMoveSpeed;
        rotSpeed = Random.Range(0f, maxRotSpeed);
    }

    private void Update()
    {
        if ((transform.position - player.position).sqrMagnitude > destroyDist * destroyDist)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage();
            Die();
        }
    }

    private void FixedUpdate()
    {
        rotAngle += rotSpeed;
        transform.rotation = Quaternion.AngleAxis(rotAngle, Vector3.forward);
    }
}
