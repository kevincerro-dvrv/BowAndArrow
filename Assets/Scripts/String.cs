using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class String : MonoBehaviour {
    public Transform topPoint;
    public Transform middlePoint;
    public Transform bottomPoint;

    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update() {
        lineRenderer.SetPositions(new Vector3[] {topPoint.position, middlePoint.position, bottomPoint.position});
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(topPoint.position, middlePoint.position);
        Gizmos.DrawLine(middlePoint.position, bottomPoint.position);
    }
}
