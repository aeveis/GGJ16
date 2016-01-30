using UnityEngine;
using System.Collections;
using System;

public class Jumping : Ritual
{
    //check att
    public float m_MaxTimeLanded = 0.5f;
    private float m_CurrentTimeLanded = 0.0f;

    //act att
    public float m_ElapsedTimeGround;
    private Rigidbody m_Rigidbody;

    /// <summary>
    /// We should have a minimal interval to check
    /// </summary>
    /// <returns></returns>
    public override bool Check(Transform p_Actor)
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

    /// <summary>
    /// For this one we are going to copy all the jumping data from the player.
    /// </summary>
    /// <param name="p_Actor"></param>
    public override void Action(Transform p_Actor)
    {
        if(m_Rigidbody == null)
            m_Rigidbody = p_Actor.GetComponent<Rigidbody>();

        //keep jumping
        if (p_Actor.position.y > PlayerController.Instance.m_GroundLevel)
        {
            m_Rigidbody.velocity += Vector3.down * Time.deltaTime * PlayerController.Instance.m_FallSpeed;
        }
        else if (p_Actor.position.y <= PlayerController.Instance.m_GroundLevel)
        {
            m_ElapsedTimeGround += Time.deltaTime;
            if (m_ElapsedTimeGround > m_MaxTimeLanded)
            {
                m_Rigidbody.velocity = Vector3.up * PlayerController.Instance.m_JumpForce;
                m_ElapsedTimeGround = 0.0f;
            }
        }
    }

    public override void OnVisionConeEnter()
    {

    }

    public override void OnVisionConeExit()
    {
    }
}
