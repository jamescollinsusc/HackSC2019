using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerMaster : MonoBehaviour {

    #region Member Variables
    private List<UnityEvent> m_OnTriggerEnterEvents = null;
    private List<UnityEvent> m_OnTriggerExitEvents = null;
    private List<Collider> m_CollidingObjects = null;
    private int m_NumObjectsInTrigger = 0;
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

    public Collider GetCollidingObject()
    {
        if (m_CollidingObjects.Count > 0) return m_CollidingObjects[m_CollidingObjects.Count - 1];
        else return null;
    }

    public int GetNumObjectsInTrigger()
    {
        return m_NumObjectsInTrigger;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Manipulators")
            || other.gameObject.layer == LayerMask.NameToLayer("Hands"))
        {
            m_NumObjectsInTrigger++;
            if (m_CollidingObjects == null) m_CollidingObjects = new List<Collider>();
            m_CollidingObjects.Add(other);
            if (m_OnTriggerEnterEvents.Count != 0) foreach (UnityEvent evt in m_OnTriggerEnterEvents) evt.Invoke();
            else Debug.LogError("There were no events registered with the trigger master for OnTriggerEnter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Manipulators")
            || other.gameObject.layer == LayerMask.NameToLayer("Hands"))
        {
            m_NumObjectsInTrigger--;
            m_CollidingObjects.Remove(other);
            if (m_OnTriggerExitEvents.Count != 0) foreach (UnityEvent evt in m_OnTriggerExitEvents) evt.Invoke();
            else Debug.LogError("There were no events registered with the trigger master for OnTriggerExit");
        }
    }

    private void OnDestroy()
    {
        if (m_OnTriggerEnterEvents != null)
            foreach (UnityEvent evt in m_OnTriggerEnterEvents) evt.RemoveAllListeners();
        if (m_OnTriggerExitEvents != null)
            foreach (UnityEvent evt in m_OnTriggerExitEvents) evt.RemoveAllListeners();
    }
}
