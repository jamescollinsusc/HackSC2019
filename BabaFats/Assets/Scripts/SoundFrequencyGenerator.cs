using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundFrequencyGenerator : AudioParent {

    #region
    [Range(1, 20000)]
    public float m_LeftChannelFrequency = 440.0f;
    [Range(1, 20000)]
    public float m_RightChannelFrequency = 440.0f;

    public float m_SampleRate = 44100.0f;
    public float m_WavelengthSeconds = 2.0f;

    private int m_TimeIndex = 0;
    #endregion

    private void Awake()
    {
        UnityEvent TriggerEnterEvent = new UnityEvent();
        TriggerEnterEvent.AddListener(() => 
        {
            m_TimeIndex = 0;
            if (!m_AudioSource.isPlaying) m_AudioSource.Play();
            else m_AudioSource.mute = false;
        });
        AddOnTriggerEnterEvent(TriggerEnterEvent);

        UnityEvent TriggerExitEvent = new UnityEvent();
        TriggerExitEvent.AddListener(() => 
        {
            if(GetNumObjectsInTrigger() == 0) m_AudioSource.mute = true;
        });
        AddOnTriggerExitEvent(TriggerExitEvent);
    }

    private void OnDisable()
    {
        m_AudioSource.Stop();
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
}
