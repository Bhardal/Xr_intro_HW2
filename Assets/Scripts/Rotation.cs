using UnityEngine;
using UnityEngine.InputSystem;

public class ConstantRotation : MonoBehaviour
{
    public InputActionReference action;
    public float degreesPerSecond = 0.00415f;
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (degreesPerSecond == 0.00415f)
            {
                degreesPerSecond = 2f;
            }
            else if (degreesPerSecond == 2f)
            {
                degreesPerSecond = 18f;
            }
            else
            {
                degreesPerSecond = 0.00415f;
            }
        };
    }

    void Update()
    {
        transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
    }
}
