using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RythmMaster : AudioParent {

    [SerializeField]
    private AnimationCurve m_RythmTrack;

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
