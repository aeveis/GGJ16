using UnityEngine;
using System.Collections;

public class NoEyeContact : Ritual
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool Check(Transform p_Actor)
    {

        return false;
    }
    public override void Action(Transform p_Actor)
    {

    }

    public override void Reset()
    {

    }
}
