using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour
{
    public Ritual m_Ritual;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Ritual.Check())
            PlayerController.Instance.Reset();
    }
}
