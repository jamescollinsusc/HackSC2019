using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerMaster : MonoBehaviour {

    #region Member Variables
    private List<UnityEvent> m_OnTriggerEnterEvents;
    private List<UnityEvent> m_OnTriggerExitEvents;
    #endregion

    #region Public Methods
    public void AddOnTriggerEnterEvent(UnityEvent enterEvent)
    {
        if (m_OnTriggerEnterEvents == null) m_OnTriggerEnterEvents = new List<UnityEvent>();
        m_OnTriggerEnterEvents.Add(enterEvent);
    }

    public void AddOnTriggerExitEvent(UnityEvent exitEvent)
    {
        if (m_OnTriggerExitEvents == null) m_OnTriggerExitEvents = new List<UnityEvent>();
        m_OnTriggerExitEvents.Add(exitEvent);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (m_OnTriggerEnterEvents.Count != 0)
        {
            foreach (UnityEvent evt in m_OnTriggerEnterEvents) evt.Invoke();
        }
        else Debug.LogError("There were no events registered with the trigger master for OnTriggerEnter");
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_OnTriggerExitEvents.Count != 0)
        {
            foreach (UnityEvent evt in m_OnTriggerExitEvents) evt.Invoke();
        }
        else Debug.LogError("There were no events registered with the trigger master for OnTriggerExit");
    }
}
