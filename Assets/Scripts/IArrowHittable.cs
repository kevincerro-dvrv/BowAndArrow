using UnityEngine;

public interface IArrowHittable
{
    void Hit(Arrow arrow, RaycastHit hit);
}
