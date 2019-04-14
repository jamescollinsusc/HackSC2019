using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OVRGrabbable))]
public class ThrownInstrumentSpawner : MonoBehaviour {

    #region Member Variables
    [SerializeField]
    GameObject m_InstrumentToSpawn = null;
    [SerializeField]
    private float m_HeightOffset = 0.0f;
#pragma warning disable 0414
    private OVRGrabbable m_GrabbingScript = null;
#pragma warning restore 0414
    #endregion

    #region Public Methods
    public void OnGrabbed()
    {
    }

    public void OnReleased()
    {
    }
    #endregion

    // Use this for initialization
    private void Start ()
    {
        m_GrabbingScript = gameObject.GetComponent<OVRGrabbable>();		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            SpawnInstrument(collision.contacts[0].point);
        }
    }

    private void SpawnInstrument(Vector3 position)
    {
        position.y += m_HeightOffset;
        GameObject spawned = Instantiate(m_InstrumentToSpawn, position, Quaternion.identity);
        spawned.GetComponent<SoundFrequencyGenerator>().m_LeftChannelFrequency = 55.0f * Random.Range(1, 10);
        spawned.GetComponent<SoundFrequencyGenerator>().m_RightChannelFrequency = 55.0f * Random.Range(1, 10);
        Destroy(gameObject);
    }
}
