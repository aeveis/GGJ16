using UnityEngine;
using System.Collections;
using System;

public class CantStop : Ritual
{
    public override void Action(Transform p_Actor)
    {
        throw new NotImplementedException();
    }

    public override bool Check(Transform p_Actor)
    {
        return PlayerController.Instance.IsMoving();
    }

    public override void OnVisionConeEnter()
    {

    }

    public override void OnVisionConeExit()
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
