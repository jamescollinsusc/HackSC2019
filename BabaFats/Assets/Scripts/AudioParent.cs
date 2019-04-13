using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioParent : MonoBehaviour {
    protected AudioSource m_AudioSource;

    void Start ()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
}
