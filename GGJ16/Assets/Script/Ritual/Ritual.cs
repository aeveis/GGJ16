using UnityEngine;
using System.Collections;

public abstract class Ritual : MonoBehaviour
{
    public abstract void OnVisionConeEnter();
    public abstract void OnVisionConeExit();
    public abstract bool Check(Transform p_Actor);  //the guard responsible in checking
    public abstract void Action(Transform p_Actor); //the civilian responsible for action
}
