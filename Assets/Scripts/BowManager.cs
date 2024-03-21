using UnityEngine;

public class BowManager : MonoBehaviour
{
    public static BowManager instance;

    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Awake() {
        instance = this;
    }

    void Start() {
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void RestorePosition() {
        // Restore velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Restore position and rotation 
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
