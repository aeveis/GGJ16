using UnityEngine;
using System.Collections;
using System;

public class NoJumping : Ritual
{
    public override bool Check()
    {
        return !PlayerController.Instance.IsJumping();
    }
}
