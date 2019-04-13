using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RythmMaster : AudioParent {

    #region Member Variables
    [SerializeField]
    private AnimationCurve m_RythmTrack;
    #endregion

    private void Awake()
    {
        UnityEvent TriggerEnterEvent = new UnityEvent();
        TriggerEnterEvent.AddListener(() => { Debug.Log("Rythm OnTriggerEnter"); });
        AddOnTriggerEnterEvent(TriggerEnterEvent);

        UnityEvent TriggerExitEvent = new UnityEvent();
        TriggerExitEvent.AddListener(() => { Debug.Log("Rythm OnTriggerExit"); });
        AddOnTriggerExitEvent(TriggerExitEvent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (m_AudioSource.isPlaying)
            {
                StartCoroutine(RunRythm());
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StopAllCoroutines();
        }
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
                m_AudioSource.Pause();
                lockPause = true;
            }
            else lockPause = false;
            if (m_RythmTrack.Evaluate(timePlayed) >= 0.99f && !lockUnpause)
            {
                m_AudioSource.UnPause();
                lockUnpause = true;
            }
            else lockUnpause = false;
            yield return null;
        }
        StartCoroutine(RunRythm());
    }
    
}
