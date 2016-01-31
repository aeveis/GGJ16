using UnityEngine;
using System.Collections;
using System;

public class SpinJump : Ritual
{

    private bool m_HasJumped;
    private bool m_HasSpinned;

    //act att
    private float m_GroundVal;
    public float m_ElapsedTimeGround;
    private Rigidbody m_Rigidbody;

    public float m_MaxTimeLanded = 0.5f;
    public float m_SpinningSpeed = 2.0f;
    private bool m_Spinning;

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
        if (m_Rigidbody == null)
            m_Rigidbody = p_Actor.GetComponent<Rigidbody>();

        //todo: save this to stop getting the ref every frame
        bool grounded = p_Actor.GetComponent<Civilian>().IsGrounded();

        //keep jumping
        if (!grounded)
        {
            m_Rigidbody.velocity += Vector3.down * Time.deltaTime * PlayerController.Instance.m_FallSpeed;
            p_Actor.Rotate(Vector3.up, 20.0f);
        }
        else
        {
            m_ElapsedTimeGround += Time.deltaTime;
            if (m_ElapsedTimeGround > m_MaxTimeLanded)
            {
                m_Rigidbody.velocity = Vector3.up * PlayerController.Instance.m_JumpForce;
                m_ElapsedTimeGround = UnityEngine.Random.Range(0.0f, m_MaxTimeLanded);
            }
        }
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
