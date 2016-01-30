using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour
{
    public Ritual m_Ritual;
    public Light m_Flashlight;

    public float m_Dist = 10.0f;
    public float m_Angle = 90.0f;

    public GameObject m_ExclamationMark;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        bool isOnSight = CheckPlayerIsOnSight();
        m_ExclamationMark.SetActive(isOnSight);
        if (isOnSight && !m_Ritual.Check())
        {
            PlayerController.Instance.Reset();
        }
    }

    private bool CheckPlayerIsOnSight()
    {
        //m_Flashlight.spotAngle
        float dist = (PlayerController.Instance.transform.position - transform.position).magnitude;
        if (dist < m_Dist)
        {
            Vector3 dirToPlayer = (PlayerController.Instance.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToPlayer);
            if (angle < m_Angle) 
            {
                RaycastHit hit;
                Debug.DrawRay(transform.position, dirToPlayer, Color.red);
                if (Physics.Raycast(transform.position, dirToPlayer, out hit, m_Dist))
                    return (hit.transform.tag == "Player") ? true : false;
            }
        }
        return false;
    }
   
}
