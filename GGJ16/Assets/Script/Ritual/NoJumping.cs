using UnityEngine;
using System.Collections;
using System;

public class NoJumping : Ritual
{
    public override bool Check(Transform p_Actor)
    {
        return !PlayerController.Instance.IsJumping();
    }

    public override void Action(Transform p_Actor)
    {
        //do nothing :)
    }

}
