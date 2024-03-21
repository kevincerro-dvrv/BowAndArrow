using UnityEngine;

public class BowManager : MonoBehaviour
{
    public static BowManager instance;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Awake() {
        instance = this;
    }

    void Start() {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void RestorePosition() {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
