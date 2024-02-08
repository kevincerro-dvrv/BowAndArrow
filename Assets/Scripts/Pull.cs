using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pull : XRBaseInteractable {
    public Transform stringPullPoint;

    public Transform startPoint;
    public Transform endPoint;

    private XRBaseInteractor interactor;

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
                stringPullPoint.position = interactor.transform.position;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float CalculatePullAmount() {
        return 0;
    }


    void OnDrawGizmos() {
        Gizmos.DrawLine(startPoint.position, endPoint.position);
    }


}
