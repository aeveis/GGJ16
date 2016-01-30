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
        return !PlayerController.Instance.CheckGuardIsOnSight(p_Actor.GetComponent<Guard>());
    }
    public override void Action(Transform p_Actor)
    {

    }
}
