using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeviceDropdown : MonoBehaviour
{
    private void Start()
    {
        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
        foreach (var device in Microphone.devices)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(device));
        }
    }
}
