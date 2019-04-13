using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundFrequencyGenerator : AudioParent {

    #region
    [Range(1, 20000)]
    public float m_LeftChannelFrequency;
    [Range(1, 20000)]
    public float m_RightChannelFrequency;

    public float m_SampleRate = 44100;
    public float m_WavelengthSeconds = 2.0f;

    private int m_TimeIndex = 0;
    #endregion

    private void Awake()
    {
        UnityEvent TriggerEnterEvent = new UnityEvent();
        TriggerEnterEvent.AddListener(() => { Debug.Log("Frequency OnTriggerEnter"); });
        AddOnTriggerEnterEvent(TriggerEnterEvent);

        UnityEvent TriggerExitEvent = new UnityEvent();
        TriggerExitEvent.AddListener(() => { Debug.Log("Frequency OnTriggerExit"); });
        AddOnTriggerExitEvent(TriggerExitEvent);
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = CreateSine(m_TimeIndex, m_LeftChannelFrequency, m_SampleRate);

            if (channels == 2)
                data[i + 1] = CreateSine(m_TimeIndex, m_RightChannelFrequency, m_SampleRate);

            m_TimeIndex++;

            //if timeIndex gets too big, reset it to 0
            if (m_TimeIndex >= (m_SampleRate * m_WavelengthSeconds))
            {
                m_TimeIndex = 0;
            }
        }
    }

    //Creates a sinewave
    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_TimeIndex = 0;
            m_AudioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            m_AudioSource.Stop();
        }
    }

}
