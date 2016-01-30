using UnityEngine;
using System.Collections;

public class VisionCone : MonoBehaviour {

    public bool m_IsPlayerOnSight;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" )
            m_IsPlayerOnSight = true;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            m_IsPlayerOnSight = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            m_IsPlayerOnSight = false;
    }

    public bool IsPlayerOnSight()
    {
        return m_IsPlayerOnSight;
    }
}
