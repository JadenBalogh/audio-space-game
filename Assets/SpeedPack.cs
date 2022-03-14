using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPack : Destroyable
{
    [SerializeField] private float duration;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out Player player))
        {
            player.AttackSpeedBoost(duration);
            Die();
        }
    }
}
