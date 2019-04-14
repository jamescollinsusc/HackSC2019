using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RythmMaster : AudioParent {

    #region Member Variables
    [SerializeField]
    private AnimationCurve m_RythmTrack = null;
    #endregion

    private void Awake()
    {
        UnityEvent TriggerEnterEvent = new UnityEvent();
        TriggerEnterEvent.AddListener(() => 
        {
            StartCoroutine(RunRythm());
        });
        AddOnTriggerEnterEvent(TriggerEnterEvent);

        UnityEvent TriggerExitEvent = new UnityEvent();
        TriggerExitEvent.AddListener(() => 
        {
            StopAllCoroutines();
        });
        AddOnTriggerExitEvent(TriggerExitEvent);
    }

    private IEnumerator RunRythm()
    {
        float timePlayed = 0.0f;
        float maxLength = m_RythmTrack.keys[m_RythmTrack.length - 1].time;
        bool lockPause = false;
        bool lockUnpause = false;

        while (timePlayed <= maxLength)
        {
            timePlayed += Time.deltaTime;
            if (m_RythmTrack.Evaluate(timePlayed) <= 0.01f && !lockPause)
            {
                m_AudioSource.mute = true;
                lockPause = true;
            }
            else lockPause = false;
            if (m_RythmTrack.Evaluate(timePlayed) >= 0.99f && !lockUnpause)
            {
                m_AudioSource.mute = false;
                lockUnpause = true;
            }
            else lockUnpause = false;
            yield return null;
        }
        StartCoroutine(RunRythm());
    }
    
}
