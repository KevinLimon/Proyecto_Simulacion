using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _WPA;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 distBetween = _WPA.position - transform.position;
        other.transform.position += distBetween;
    }
}
