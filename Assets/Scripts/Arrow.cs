using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Arrow : MonoBehaviour {

    private XRInteractionManager interactionManager;
    private XRGrabInteractable arrowGrabInteractable;
    private XRSocketInteractor bowStringSocket;

    public delegate void OnStringCapturedDelegate(Transform stringMiddlePoint);
    public OnStringCapturedDelegate OnStringCaptured;

    void Start() {
        bowStringSocket = GetComponent<XRSocketInteractor>();
        arrowGrabInteractable = GetComponent<XRGrabInteractable>(); 

        bowStringSocket.selectEntered.AddListener(StringCaptured);
        bowStringSocket.enabled = false;

        arrowGrabInteractable.selectExited.AddListener(ReleaseArrow);
        arrowGrabInteractable.selectEntered.AddListener(GrabArrow);

        GameObject xrIMContainer = GameObject.Find("XR Interaction Manager");
        interactionManager = xrIMContainer.GetComponent<XRInteractionManager>();

    }

    void LateUpdate() {
        if(bowStringSocket.interactablesSelected.Count != 0) {
            Transform stringTransform = ((MonoBehaviour)bowStringSocket.interactablesSelected[0]).GetComponent<Pull>().stringPullPoint.transform;

            transform.position = stringTransform.position;
            transform.rotation = stringTransform.rotation;            
        }
    }


    public void GrabArrow(SelectEnterEventArgs args) {
        bowStringSocket.enabled = true;
    }

    public void ReleaseArrow(SelectExitEventArgs args) {
        Debug.Log("[Arrow] ReleaseArrow");
        
        if(bowStringSocket.interactablesSelected.Count != 0) {
            Debug.Log("[Arrow] ReleaseArrow Socket ten agarrada corda");
            interactionManager.CancelInteractableSelection(bowStringSocket.interactablesSelected[0]);
            TrowArrow();
        }

        bowStringSocket.enabled = false;

    }

    public void StringCaptured(SelectEnterEventArgs args) {
        if (OnStringCaptured != null) {
            Pull stringPull = args.interactable.GetComponent<Pull>();
            OnStringCaptured(stringPull.stringPullPoint);
        }
    }

    private void TrowArrow() {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * 100, ForceMode.Impulse);
    }
}
