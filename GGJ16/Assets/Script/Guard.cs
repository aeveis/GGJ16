using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour
{
    public GameObject m_VisionCone;
    public VisionCone m_Cone;
    public Ritual m_Ritual;

    // Use this for initialization
    void Start()
    {
        m_VisionCone = Instantiate(m_VisionCone) as GameObject;
        m_VisionCone.transform.parent = transform;
        m_VisionCone.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);

        m_Cone = m_VisionCone.GetComponent<VisionCone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Ritual.Check() && m_Cone.IsPlayerOnSight())
        {
            PlayerController.Instance.Reset();
        }
    }
    
   
}
