using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioParent : TriggerEventListener {

    #region Member Variables
    protected AudioSource m_AudioSource;
    #endregion

    private void Start ()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

}
