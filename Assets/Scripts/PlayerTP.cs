using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTP : MonoBehaviour
{
    public InputActionReference action;
    public string PosName = "Start";
    private Vector3 StartPos = new Vector3(0, 0, 0);
    private Vector3 WorldViewPos = new Vector3(0, 7.5f, -20);
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (PosName == "Start")
            {
                PosName = "WorldWiew";
                transform.position = WorldViewPos;
            }
            else
            {
                PosName = "Start";
                transform.position = StartPos;
            }

        };

    }
}
