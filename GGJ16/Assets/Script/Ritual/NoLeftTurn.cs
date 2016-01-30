using UnityEngine;
using System.Collections;
using System;

public class NoLeftTurn : Ritual
{

    //check att
    //private Vector3 m_lastForward = 0.0f;
    int m_oldHeadingAngle = 0;

    //act att
    private Rigidbody m_Rigidbody;
    public float m_anglesPerSecond;

    /// <summary>
    /// If your forward is ever more left than it was, you failed
    /// </summary>
    /// <returns></returns>
    public override bool Check(Transform p_Actor) //false is you're caught
    {
        Vector3 forward = PlayerController.Instance.transform.forward;
        forward.y = 0;
		int headingAngle = (int)(Quaternion.LookRotation(forward).eulerAngles.y);

        //Debug.Log("headingAngle = " + headingAngle);

		bool good = true;

		if (headingAngle == 0 && m_oldHeadingAngle == 270){
			Debug.Log("1");
			good = true;
		} else if (headingAngle == 270 && m_oldHeadingAngle == 0){
			Debug.Log("2");
			good = false;
		} else if (headingAngle == 0 && m_oldHeadingAngle == 315) {
			Debug.Log("3");
			good = true;
		} else if (headingAngle == 315 && m_oldHeadingAngle == 0) {
			Debug.Log("4");
			good = false;
		} else if (Math.Abs(headingAngle - m_oldHeadingAngle) > 95) {
			good = false;
			Debug.Log("5");
		} else if (headingAngle < m_oldHeadingAngle){ 
			Debug.Log("6");
			good = false;
		}
		m_oldHeadingAngle = headingAngle;
        return good;
        

    }

    /// <summary>
    /// Spin in a circle to the right
    /// </summary>
    /// <param name="p_Actor"></param>
    public override void Action(Transform p_Actor)
    {
        if (m_Rigidbody == null)
            m_Rigidbody = p_Actor.GetComponent<Rigidbody>();

        //keep rotating
        p_Actor.transform.Rotate(new Vector3(0.0f, m_anglesPerSecond * Time.deltaTime, 0.0f));
    }

    public override void OnVisionConeEnter()
    {
		Vector3 forward = PlayerController.Instance.transform.forward;
		forward.y = 0;
		int headingAngle = (int)(Quaternion.LookRotation(forward).eulerAngles.y);
		m_oldHeadingAngle = headingAngle;
    }

    public override void OnVisionConeExit()
    {
    }
}
