using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUI : MonoBehaviour
{
    public Transform whatToLookAt;

    public Transform whatToMove;

    private void Update()
    {
        whatToMove.LookAt(whatToLookAt.position - whatToLookAt.transform.rotation * Vector3.back, whatToLookAt.transform.rotation * Vector3.up);
    }
}
