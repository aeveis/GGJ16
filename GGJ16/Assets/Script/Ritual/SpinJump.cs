using UnityEngine;
using System.Collections;
using System;

public class SpinJump : Ritual
{

    private bool m_HasJumped;
    private bool m_HasSpinned;

    // Use this for initialization
    void Start()
    {

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
        if (!m_HasJumped && PlayerController.Instance.IsJumping())
        {
            //Debug.Log("m_HasJumped");
            m_HasJumped = true;
        }

        if (!m_HasSpinned && PlayerController.Instance.IsSpinning() && PlayerController.Instance.IsJumping())
        {
            //Debug.Log("m_HasSpinned");
            m_HasSpinned = true;
        }

        //has landed
        if (m_HasJumped && !PlayerController.Instance.IsJumping())
        {
            bool result = m_HasSpinned;

            m_HasSpinned = false;
            m_HasJumped = false;
            //Debug.Log("landed spin["+ m_HasSpinned + "]");
            return result;
        }

        return true;
    }

    public override void OnVisionConeEnter()
    {
        m_HasJumped = false;
        m_HasSpinned = false;
    }

    public override void OnVisionConeExit()
    {
        m_HasJumped = false;
        m_HasSpinned = false;
    }


}
