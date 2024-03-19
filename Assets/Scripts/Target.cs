using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Target : MonoBehaviour, IArrowHittable {

    private float internalCircleRadius = 0.037f;
    private float externalRadius = 0.49f;
    private float stripeWidth;
    private int numberOfStripes = 9;

    void Start()
    {
        stripeWidth = (externalRadius - internalCircleRadius) / numberOfStripes;
    }

    public void Hit(Arrow arrow, RaycastHit hit) {
        Debug.Log("[Target] " + hit.point);

        Vector3 impactPoint = transform.InverseTransformPoint(hit.point);
        impactPoint.x = 0;

        float impactDistance = impactPoint.magnitude;
        int points = 0;
        if (impactDistance <= internalCircleRadius) {
            points = 10;
        } else if (impactDistance <= externalRadius) {
            impactDistance -= internalCircleRadius;
            int stripeIndex = (int)Mathf.Floor(impactDistance/stripeWidth);
            points = numberOfStripes - stripeIndex;
        }

        GameManager.instance.AddScore(points);
    }
}
