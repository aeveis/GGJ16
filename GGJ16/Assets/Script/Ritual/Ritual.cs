using UnityEngine;
using System.Collections;

public abstract class Ritual : MonoBehaviour
{
    public abstract bool Check();
    public abstract void Action(Transform p_Actor);
    public abstract void Reset();
}
