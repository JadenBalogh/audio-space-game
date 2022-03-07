using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private bool showDebug = false;

    private string device = "";
    private float audioInput = 0;
    public float AudioInput { get => audioInput; }

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SelectDevice(0);
        StartCoroutine(DisplayDiff());
    }

    private void Update()
    {
        float[] spectrum = new float[64];

        // AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        AudioListener.GetOutputData(spectrum, 0);

        float leftTotal = 0;
        for (int i = 0; i < spectrum.Length / 2; i++)
        {
            leftTotal += spectrum[i];
        }
        float rightTotal = 0;
        for (int i = spectrum.Length / 2; i < spectrum.Length; i++)
        {
            rightTotal += spectrum[i];
        }
        audioInput = leftTotal - rightTotal;
    }

    private IEnumerator DisplayDiff()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (showDebug)
            {
                debugText.text = audioInput.ToString("0.##");
            }
            else
            {
                debugText.text = "";
            }
        }
    }

    public void Stop()
    {
        Microphone.End(device);
    }

    public void SelectDevice(int deviceIdx)
    {
        if (device != "") Microphone.End(device);
        device = Microphone.devices[deviceIdx];
        Debug.Log(device);
        audioSource.Stop();
        audioSource.clip = Microphone.Start(device, true, 600, 44100);
        audioSource.Play();
    }
}
