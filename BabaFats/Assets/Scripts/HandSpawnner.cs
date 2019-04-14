using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpawnner : MonoBehaviour {
    [SerializeField]
    private GameObject m_Hitter;
    [SerializeField]
    private GameObject m_EggSpawner;
    [SerializeField]
    private float m_EggSpeed;
    [SerializeField]
    private OVRInput.Button m_HitterSpawn;
    [SerializeField]
    private OVRInput.Button m_EggSpawnSpawner;
    [SerializeField]
    private OVRInput.Button[] m_FallButton;
    private List<GameObject> SpawnnedObjs;
	
	// Update is called once per frame
	private void Update ()
    {
        if (OVRInput.GetDown(m_HitterSpawn))
        {
            if (SpawnnedObjs == null) SpawnnedObjs = new List<GameObject>();
            SpawnnedObjs.Add(Instantiate(m_Hitter, transform.position, Quaternion.identity));
        }
        if (OVRInput.GetDown(m_FallButton[0]))
        {
            MakeFall();
        }
        if (OVRInput.GetDown(m_FallButton[1]))
        {
            MakeFall();
        }
        if (OVRInput.GetDown(m_EggSpawnSpawner))
        {
            SpawnSpawner();
        }
	}

    private void MakeFall()
    {
        if (SpawnnedObjs == null) return;
        foreach (GameObject obj in SpawnnedObjs)
        {
            if (obj != null) obj.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void SpawnSpawner()
    {
        GameObject egg = Instantiate(m_EggSpawner, transform.position, Quaternion.identity);
        egg.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * m_EggSpeed;
    }
}
