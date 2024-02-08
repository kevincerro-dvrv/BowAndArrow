using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowPull : XRBaseInteractable
{
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

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic && interactor != null) {
            float pullAmount = CalculatePullAmount();
            stringPullPoint.position = interactor.transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float CalculatePullAmount()
    {
        Vector3 b = startPoint.position;
        Vector3 a = endPoint.position;
        Vector3 c = stringPullPoint.position;

        return 0f;
        //return ((a - b).Multiply(c- b));
    }
}
