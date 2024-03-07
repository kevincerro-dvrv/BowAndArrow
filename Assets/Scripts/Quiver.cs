using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRBaseInteractable
{
    public GameObject arrowPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        base.OnSelectEntered(args);

        IXRSelectInteractor interactor = args.interactorObject;
        XRGrabInteractable arrow = Instantiate(arrowPrefab, interactor.transform.position, interactor.transform.rotation).GetComponent<XRGrabInteractable>();
        interactionManager.SelectEnter(args.interactorObject, arrow);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
    }
}
