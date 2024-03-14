using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArrowHittable {
    public void Hit(Arrow arrow, RaycastHit hit);
}
