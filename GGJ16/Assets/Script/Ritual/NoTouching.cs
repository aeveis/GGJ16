using UnityEngine;
using System.Collections;
using System;

public class NoTouching : Ritual
{

    public bool m_CheckGuards = true;
    public bool m_CheckCivilians = true;

    public float m_Proximity = 2.0f;

    private Civilian[] m_AllCivilians;
    private Guard[] m_AllGuards;

    // Use this for initialization
    void Start()
    {
        //m_AllCivilians = GameObject.FindObjectsOfType<Civilian>();
        //m_AllGuards = GameObject.FindObjectsOfType<Guard>();

        //Debug.Log(m_AllGuards.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Action(Transform p_Actor)
    {

    }

    public override bool Check(Transform p_Actor)
    {
        if (m_CheckCivilians)
        {
            foreach (Civilian c in m_AllCivilians)
            {
                if ((PlayerController.Instance.transform.position - c.transform.position).magnitude < m_Proximity)
                    return false;
            }
        }

        if (m_CheckGuards)
        {
            foreach (Guard g in m_AllGuards)
            {
                if ((PlayerController.Instance.transform.position - g.transform.position).magnitude < m_Proximity)
                    return false;
            }
        }

        return true;
    }

    public override void OnVisionConeEnter()
    {
        //do we need to check if the environment changes?

        m_AllCivilians = GameObject.FindObjectsOfType<Civilian>();
        m_AllGuards = GameObject.FindObjectsOfType<Guard>();
        //Debug.Log(m_AllGuards.Length);
    }

    public override void OnVisionConeExit()
    {

    }
}
