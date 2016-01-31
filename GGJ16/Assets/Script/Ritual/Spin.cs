using UnityEngine;
using System.Collections;
using System;

public class Spin : Ritual
{


    

    //action vars
    public float m_timeNotSpinning;
	public float m_timeSpinning;
	public float m_SpinningSpeed = 2.0f;
	private float m_ElapsedTime;
	private Rigidbody m_Rigidbody;
	private bool m_spinning;

	//checck vars
	private bool m_hasSpun;
    public float m_MaxTimeNotSpinning = 0.25f;
    
    

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

        //keep spinning
        if (m_spinning)
        {
			p_Actor.Rotate(Vector3.up, m_SpinningSpeed);
		} 
        m_ElapsedTime += Time.deltaTime;
		if (m_spinning && m_ElapsedTime > m_timeSpinning)
        {
			m_ElapsedTime = 0;
			m_spinning = false;
		}
		if (!m_spinning && m_ElapsedTime > m_timeNotSpinning)
		{
			m_ElapsedTime = 0;
			m_spinning = true;
		}
    }

  

    public override bool Check(Transform p_Actor)
    {
		
		m_ElapsedTime += Time.deltaTime;
		if (m_ElapsedTime > m_MaxTimeNotSpinning)
		{
			m_ElapsedTime = 0;
			if (m_hasSpun){
				m_hasSpun = false;
				return true;

			} else {

				return false;
			}
		}

        if (PlayerController.Instance.IsSpinning())
        {
            m_hasSpun = true;
        }
		return true;
	}

    public override void OnVisionConeEnter()
    {
		m_hasSpun = false;
		m_ElapsedTime = 0;
    }

    public override void OnVisionConeExit()
    {
		m_hasSpun = false;
		m_ElapsedTime = 0;
    }


}
