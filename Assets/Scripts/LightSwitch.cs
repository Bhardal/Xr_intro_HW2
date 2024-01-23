using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    public Light light;
    public InputActionReference action;
    public string LightColor = "White";
    void Start()
    {
        light = GetComponent<Light>();
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (LightColor == "White")
            {
                LightColor = "Random";
                light.color = new Color(Random.value, Random.value, Random.value);
            }
            else{
                LightColor = "White";
                light.color = Color.white;
            }
        };
    }
}
