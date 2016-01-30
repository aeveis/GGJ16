using UnityEngine;
using System.Collections;

public class NoLeftTurn : Ritual {

	//check att
	//private Vector3 m_lastForward = 0.0f;
	float m_oldHeadingAngle = 0.0f;

	//act att
	private Rigidbody m_Rigidbody;
	public float m_anglesPerSecond;

	/// <summary>
	/// If your forward is ever more left than it was, you failed
	/// </summary>
	/// <returns></returns>
	public override bool Check(Transform p_Actor)
	{
		Vector3 forward = PlayerController.Instance.transform.forward;
		forward.y = 0; 
		float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
		if (headingAngle < m_oldHeadingAngle && headingAngle != 0)
		{
			return false;
		} else {
			m_oldHeadingAngle = headingAngle;
			return true;
		}

	}

	/// <summary>
	/// Spin in a circle to the right
	/// </summary>
	/// <param name="p_Actor"></param>
	public override void Action(Transform p_Actor)
	{
		if(m_Rigidbody == null)
			m_Rigidbody = p_Actor.GetComponent<Rigidbody>();

		//keep rotating
		p_Actor.transform.Rotate( new Vector3(0.0f, m_anglesPerSecond * Time.deltaTime, 0.0f));
	}

}
