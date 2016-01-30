using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Debug.Log(" NEXT LEVEL!");
    }
}
