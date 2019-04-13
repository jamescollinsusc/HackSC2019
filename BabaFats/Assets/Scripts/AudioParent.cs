using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(TriggerMaster))]
public class AudioParent : MonoBehaviour {

    #region Member Variables
    protected AudioSource m_AudioSource;
    protected TriggerMaster m_TriggerMaster;
    private List<UnityEvent> m_OnTriggerEnterEvents;
    private List<UnityEvent> m_OnTriggerExitEvents;
    #endregion

    protected void AddOnTriggerEnterEvent(UnityEvent enterEvent)
    {
        if (m_OnTriggerEnterEvents == null) m_OnTriggerEnterEvents = new List<UnityEvent>();
        m_OnTriggerEnterEvents.Add(enterEvent);
    }

    protected void AddOnTriggerExitEvent(UnityEvent exitEvent)
    {
        if (m_OnTriggerExitEvents == null) m_OnTriggerExitEvents = new List<UnityEvent>();
        m_OnTriggerExitEvents.Add(exitEvent);
    }

    private void Start ()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_TriggerMaster = GetComponent<TriggerMaster>();
        foreach (UnityEvent evt in m_OnTriggerEnterEvents)
        {
            m_TriggerMaster.AddOnTriggerEnterEvent(evt);
        }
        foreach (UnityEvent evt in m_OnTriggerExitEvents)
        {
            m_TriggerMaster.AddOnTriggerExitEvent(evt);
        }
    }

}
