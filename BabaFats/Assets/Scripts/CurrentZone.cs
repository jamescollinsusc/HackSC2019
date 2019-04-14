using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class CurrentZone : MonoBehaviour {
    private List<Rigidbody> m_AllObjs;
    [SerializeField]
    private float m_VelocityScalar = 0.0f;
    [SerializeField]
    private OVRInput.Axis2D m_RightStick;

    private void OnTriggerEnter(Collider other)
    {
        if (m_AllObjs == null) m_AllObjs = new List<Rigidbody>();
        if (other.GetComponent<Rigidbody>())
            m_AllObjs.Add(other.gameObject.GetComponent<Rigidbody>());
    }

    private void ApplyVelocities(Vector3 direction, float scalar)
    {
        foreach (Rigidbody rb in m_AllObjs)
        {
            if (rb != null) rb.velocity = direction * scalar;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 axisValues = OVRInput.Get(m_RightStick);
        if(axisValues.x >= 0.95f || axisValues.x <= -0.95f || axisValues.y >= 0.95f || axisValues.y <= -0.95f)
        {
            Vector3 velVec = Vector3.zero;
            velVec.x = axisValues.x;
            velVec.z = axisValues.y;
            ApplyVelocities(velVec, m_VelocityScalar);
        }
        if (OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two)
                         && OVRInput.Get(OVRInput.Button.Three) && OVRInput.Get(OVRInput.Button.Four)
                         && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)
                         && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)
                         && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)
                         && OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
	}
}
