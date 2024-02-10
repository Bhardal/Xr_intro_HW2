using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    private bool firstGrab = false;
    public Vector3 OldGPos;
    public Vector3 OldHPos;
    public Quaternion OldGRota;
    public Quaternion OldHRota;
    bool grabbing = false;

    private void Start()
    {
        action.action.Enable();

        // Find the other hand
        foreach(CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                if (!firstGrab)
                {
                    firstGrab = true;
                    // Position
                    OldGPos = grabbedObject.position;
                    OldHPos = transform.position;
                    // Rotation
                    OldGRota = grabbedObject.rotation;
                    OldHRota = transform.rotation;
                }
                // Position
                grabbedObject.position += transform.position - OldHPos;
                OldGPos = grabbedObject.position;
                OldHPos = transform.position;
                // Rotation

                grabbedObject.rotation = transform.rotation;
                var currentRotaAngles = transform.eulerAngles;
                var oldRotaAngles = OldHRota.eulerAngles;
                var x = grabbedObject.eulerAngles.x + currentRotaAngles.x-oldRotaAngles.x;
                var y = grabbedObject.eulerAngles.y + currentRotaAngles.y-oldRotaAngles.y;
                var z = grabbedObject.eulerAngles.z + currentRotaAngles.z-oldRotaAngles.z;
                if(x > 180)
                    x -= 360;
                if(x < -180)
                    x += 360;
                if(y > 180)
                    y -= 360;
                if(y < -180)
                    y += 360;
                if(z > 180)
                    z -= 360;
                if(z < -180)
                    z += 360;

                Vector3 futurAngle = new(x, y, z);
                grabbedObject.eulerAngles = futurAngle;

                OldGRota = grabbedObject.rotation;
                OldHRota = transform.rotation;
            }
        }
        // If let go of button, release object
        else if (grabbedObject)
        {
            grabbedObject = null;
            firstGrab = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
    }
}
