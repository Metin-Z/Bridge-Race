using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI"))
        {
            other.transform.position = new Vector3(0, 5, 0);
        }
    }
}
