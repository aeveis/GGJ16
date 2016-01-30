using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Application.LoadLevel(Application.loadedLevel + 1);
            Debug.Log(" NEXT LEVEL!");
        }
    }
}
