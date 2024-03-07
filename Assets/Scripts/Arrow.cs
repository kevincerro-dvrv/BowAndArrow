using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Arrow : MonoBehaviour {

    public Transform arrowTip;

    public LayerMask hittableLayers;

    private bool flying;
    private Vector3 previousPosition;

    private XRInteractionManager interactionManager;
    private XRGrabInteractable arrowGrabInteractable;
    private XRSocketInteractor bowStringSocket;
    private Rigidbody rb;

    private float maxForce = 20f;

    public delegate void OnStringCapturedDelegate(Transform stringMiddlePoint);
    public OnStringCapturedDelegate OnStringCaptured;


    void Start() {
        bowStringSocket = GetComponent<XRSocketInteractor>();
        arrowGrabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        bowStringSocket.selectEntered.AddListener(StringCaptured);

        //bowStringSocket.enabled = false;

        arrowGrabInteractable.selectExited.AddListener(ReleaseArrow);
        arrowGrabInteractable.selectEntered.AddListener(GrabArrow);

        GameObject xrIMContainer = GameObject.Find("XR Interaction Manager");
        interactionManager = xrIMContainer.GetComponent<XRInteractionManager>();

        flying = false;
    }

    void LateUpdate() {
        if(bowStringSocket.interactablesSelected.Count != 0) {
            Transform stringTransform = ((MonoBehaviour)bowStringSocket.interactablesSelected[0]).GetComponent<Pull>().stringPullPoint.transform;

            transform.position = stringTransform.position;
            transform.rotation = stringTransform.rotation;            
        }
    }

    private void StringCaptured(SelectEnterEventArgs args) {
        if(OnStringCaptured != null) {
            Pull stringPull = args.interactable.GetComponent<Pull>();
            OnStringCaptured(stringPull.stringPullPoint);
        }
    }


    public void GrabArrow(SelectEnterEventArgs args) {
        bowStringSocket.enabled = true;
        UnfreezeArrow();
    }

    public void ReleaseArrow(SelectExitEventArgs args) {
        //Debug.Log("[Arrow] ReleaseArrow");
        
        if(bowStringSocket.interactablesSelected.Count != 0) {
            Debug.Log("[Arrow] ReleaseArrow Socket ten agarrada corda");
            //Obtemos a referencia ó compoñente Pull da corda
            Pull pull = bowStringSocket.interactablesSelected[0].transform.GetComponent<Pull>();
            float pullAmount = pull.PullAmount;
            //Liberamos a corda
            interactionManager.CancelInteractableSelection(bowStringSocket.interactablesSelected[0]);

            TrowArrow(pullAmount * maxForce);
        }

        bowStringSocket.enabled = false;
    }

    void Update() {
        if(flying) {
            if(Physics.Linecast(previousPosition, arrowTip.position, out RaycastHit hit, hittableLayers, QueryTriggerInteraction.Ignore)) {
                FreezeArrow();
                flying = false;
                hit.collider.gameObject.GetComponent<IArrowHittable>()?.Hit(this, hit);
            }

            previousPosition = arrowTip.position;
        }
    }

    void FixedUpdate() {
        if (flying && rb.velocity.z > 0.5f) {
            transform.forward = rb.velocity;
        }
    }

    private void FreezeArrow() {
        rb.isKinematic = true;
    }

    private void UnfreezeArrow() {
        rb.isKinematic = false;
    }

    private void TrowArrow(float force) {
        rb.isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        flying = true;
        previousPosition = arrowTip.position;
    }
   
}
