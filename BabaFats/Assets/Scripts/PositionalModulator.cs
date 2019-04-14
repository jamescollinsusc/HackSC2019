using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PositionalModulator : AudioParent {
    private enum InstrumentType { Zabolee, NotZabolee }
    [SerializeField]
    private InstrumentType m_ThisInstrumentType;

    private CapsuleCollider m_ZaboleeCollider = null;

    private void Awake()
    {
        OnlyHookUpWrappers();
    }
	
	// Update is called once per frame
	void Update () {
		if (m_IsInsideTrigger)
        {
            if (m_ThisInstrumentType == InstrumentType.Zabolee && m_ZaboleeCollider == null)
                m_ZaboleeCollider = GetComponent<CapsuleCollider>();
            if (m_ThisInstrumentType == InstrumentType.Zabolee)
            {
                Collider mostRecent = GetMostRecentCollider();
                if (mostRecent == null) return;
                float yDist = mostRecent.transform.position.y - transform.position.y;
                float height = m_ZaboleeCollider.height;
                float t = (yDist + (height / 2.0f))/height;
                m_AudioSource.pitch = t;
                if (mostRecent.tag == "Left")
                {
                    float bounded = mostRecent.transform.localRotation.eulerAngles.z / 180.0f;
                    m_AudioSource.panStereo = bounded;
                }
                else if (mostRecent.tag == "Right")
                {
                    float bounded = mostRecent.transform.localRotation.eulerAngles.z / 180.0f;
                    m_AudioSource.panStereo = bounded;
                }
                
            }  
        }
	}
}
