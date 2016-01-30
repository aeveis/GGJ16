using UnityEngine;
using System.Collections;

public class NoLeftTurn : Ritual {

	//check att
	//private Vector3 m_lastForward = 0.0f;

	//act att
	private Rigidbody m_Rigidbody;

	/// <summary>
	/// If your forward is ever more left than it was, you failed
	/// </summary>
	/// <returns></returns>
	public override bool Check(Transform p_Actor)
	{
		Debug.Log("player rotation : " + PlayerController.Instance.transform.rotation);
		return true;
	}

	/// <summary>
	/// Spin in a circle to the right
	/// </summary>
	/// <param name="p_Actor"></param>
	public override void Action(Transform p_Actor)
	{
		if(m_Rigidbody == null)
			m_Rigidbody = p_Actor.GetComponent<Rigidbody>();

		//keep jumping
		/*
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
		*/
	}

}
