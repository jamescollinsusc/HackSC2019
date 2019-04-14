using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TriggerMaster))]
public class TriggerEventListener : MonoBehaviour {

    #region Member Variables
    protected bool m_IsInsideTrigger = false;
    private TriggerMaster m_TriggerMaster = null;
    private List<UnityEvent> m_OnTriggerEnterEvents = null;
    private List<UnityEvent> m_OnTriggerExitEvents = null;
    #endregion

    #region Protected Methods
    protected void AddOnTriggerEnterEvent(UnityEvent enterEvent)
    {
        if (m_OnTriggerEnterEvents == null) m_OnTriggerEnterEvents = new List<UnityEvent>();
        if (m_TriggerMaster == null) m_TriggerMaster = GetComponent<TriggerMaster>();

        enterEvent.AddListener(OnEnterWrapper);
        m_OnTriggerEnterEvents.Add(enterEvent);
        m_TriggerMaster.AddOnTriggerEnterEvent(enterEvent);
    }

    protected void AddOnTriggerExitEvent(UnityEvent exitEvent)
    {
        if (m_OnTriggerExitEvents == null) m_OnTriggerExitEvents = new List<UnityEvent>();
        if (m_TriggerMaster == null) m_TriggerMaster = GetComponent<TriggerMaster>();

        exitEvent.AddListener(OnExitWrapper);
        m_OnTriggerExitEvents.Add(exitEvent);
        m_TriggerMaster.AddOnTriggerExitEvent(exitEvent);
    }

    protected void OnlyHookUpWrappers()
    {
        if (m_OnTriggerEnterEvents == null) m_OnTriggerEnterEvents = new List<UnityEvent>();
        if (m_OnTriggerExitEvents == null) m_OnTriggerExitEvents = new List<UnityEvent>();
        if (m_TriggerMaster == null) m_TriggerMaster = GetComponent<TriggerMaster>();
        UnityEvent enterEvent = new UnityEvent();
        UnityEvent exitEvent = new UnityEvent();
        enterEvent.AddListener(OnEnterWrapper);
        exitEvent.AddListener(OnExitWrapper);
        m_TriggerMaster.AddOnTriggerEnterEvent(enterEvent);
        m_TriggerMaster.AddOnTriggerExitEvent(exitEvent);
    }

    protected int GetNumObjectsInTrigger()
    {
        return m_TriggerMaster.GetNumObjectsInTrigger();
    }

    protected Collider GetMostRecentCollider()
    {
        return m_TriggerMaster.GetCollidingObject();
    }
    #endregion

    private void OnEnterWrapper()
    {
        m_IsInsideTrigger = true;
    }

    private void OnExitWrapper()
    {
        m_IsInsideTrigger = false;
    }
}
