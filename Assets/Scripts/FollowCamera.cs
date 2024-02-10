using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStuff : MonoBehaviour
{
    public Transform Cam;
    public Transform ZoomyStuff;
    private void Start()
    {

    }

    void Update()
    {
        Vector3 CamPos = ZoomyStuff.InverseTransformPoint(Cam.position);
        // transform.position = ZoomyStuff.TransformPoint(new Vector3(CamPos.x, CamPos.y, CamPos.z));
        transform.position = ZoomyStuff.position;

        transform.LookAt(ZoomyStuff.TransformPoint(new Vector3(-CamPos.x, -CamPos.y, -CamPos.z)), ZoomyStuff.up);

    }

}
