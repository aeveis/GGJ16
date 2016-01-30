using UnityEngine;
using System.Collections;
using System;

public class Jumping : Ritual
{
    public float m_MaxTimeLanded = 0.5f;
    private float m_CurrentTimeLanded = 0.0f;

    public override bool Check()
    {
        if (!PlayerController.Instance.IsJumping())
        {
            m_CurrentTimeLanded += Time.deltaTime;
            if (m_CurrentTimeLanded > m_MaxTimeLanded)
            {
                return false;
            }
        }
        else
        {
            m_CurrentTimeLanded = 0.0f;
        }
        return true;
    }
}
