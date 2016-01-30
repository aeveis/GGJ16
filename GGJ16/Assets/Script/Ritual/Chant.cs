using UnityEngine;
using System.Collections;
using System;

public class Chant : Ritual
{
	public string chantPattern ="-. -.";
	public float startBufferTime = .2f;
	public float chantSpeed = .5f;
	public override bool Check(Transform p_Actor)
	{
		return !PlayerController.Instance.IsJumping();
	}

	public override void Action(Transform p_Actor)
	{
		//do nothing :)
	}

}
