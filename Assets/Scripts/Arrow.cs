using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Arrow : MonoBehaviour {

    public Transform arrowTip;
    public LayerMask layerMask;

    private bool flying = false;
    private Vector3 previousPosition;

    private XRInteractionManager interactionManager;
    private XRGrabInteractable arrowGrabInteractable;
    private XRSocketInteractor bowStringSocket;

    public delegate void OnStringCapturedDelegate(Transform stringMiddlePoint);
    public OnStringCapturedDelegate OnStringCaptured;

    private float maxForce = 40f;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();

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

            //Obtenemos a referencia o compoñente pull da corda
            Pull pull = bowStringSocket.interactablesSelected[0].transform.GetComponent<Pull>();
            float pullAmount = pull.PullAmount;

            //Liberamos a corda
            interactionManager.CancelInteractableSelection(bowStringSocket.interactablesSelected[0]);

            TrowArrow(pullAmount * maxForce);
        }

        bowStringSocket.enabled = false;

    }

    public void StringCaptured(SelectEnterEventArgs args) {
        if (OnStringCaptured != null) {
            Pull stringPull = args.interactable.GetComponent<Pull>();
            OnStringCaptured(stringPull.stringPullPoint);
        }
    }

    void Update() {
        if (flying) {
            if (previousPosition != null) {
                if (Physics.Linecast(previousPosition, transform.position, out RaycastHit hit)) {
                    FreezeArrow();
                    flying = false;
                }
            }

            previousPosition = transform.position;
        }


    }

    private void TrowArrow(float force) {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * force, ForceMode.Impulse);
        flying = true;
    }

    private void FreezeArrow()
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
