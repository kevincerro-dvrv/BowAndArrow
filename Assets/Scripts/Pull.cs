using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pull : XRBaseInteractable {
    public Transform stringPullPoint;

    public Transform startPoint;
    public Transform endPoint;

    private XRBaseInteractor interactor;

    private Vector3 startToEndVector;

    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        base.OnSelectEntered(args);
        Debug.Log("[Pull] OnSelectEntered");
        interactor = (XRBaseInteractor)args.interactorObject;
    }

    protected override void OnSelectExited(SelectExitEventArgs args) {
        base.OnSelectExited(args);
        Debug.Log("[Pull] OnSelectExited");
        interactor = null;
        stringPullPoint.position = startPoint.position;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase) {
        base.ProcessInteractable(updatePhase);
        if(interactor != null) {
            if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic) {
                float pullAmount = CalculatePullAmount();
                stringPullPoint.position = startPoint.position + startToEndVector * pullAmount;
            }
        }
    }

    private float CalculatePullAmount() {
        startToEndVector = endPoint.position - startPoint.position;
        float maxDistance = startToEndVector.magnitude;

        Vector3 startToInteractorVector = interactor.transform.position - startPoint.position;
        float projectedDistance = Vector3.Dot(startToEndVector, startToInteractorVector) / maxDistance;

        return Mathf.Clamp(projectedDistance / maxDistance, 0f, 1f);
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(startPoint.position, endPoint.position);
    }
}
