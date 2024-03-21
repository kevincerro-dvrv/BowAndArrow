using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticController : MonoBehaviour
{
    public static HapticController instance;
    private ActionBasedController controller;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    public void TriggerHaptic(float amplitude = 0.25f, float duration = 0.25f)
    {
        controller.SendHapticImpulse(amplitude, duration);
    }
}
