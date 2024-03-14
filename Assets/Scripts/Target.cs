using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IArrowHittable {
    public void Hit(Arrow arrow, RaycastHit hit) {
        Debug.Log("[Target] " + hit.point);
    }
}
