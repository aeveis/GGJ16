using UnityEngine;
using System.Collections;

public class Civilian : MonoBehaviour
{

    public Ritual m_Ritual;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Ritual.Action(transform);
    }
}
