using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private RectTransform fill;

    public void UpdateDisplay(Player player)
    {
        float healthPercent = Mathf.Max((float)player.Health / player.MaxHealth, 0);
        fill.anchorMax = new Vector2(healthPercent, fill.anchorMax.y);
    }
}
