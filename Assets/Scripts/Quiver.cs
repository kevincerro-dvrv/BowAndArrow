using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRBaseInteractable {
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
        Debug.Log("Quiver.OnSelectEntered");

        base.OnSelectEntered(args);

        XRGrabInteractable arrow = Instantiate(arrowPrefab, args.interactorObject.transform.position, args.interactorObject.transform.rotation).GetComponent<XRGrabInteractable>();
        interactionManager.SelectEnter(args.interactorObject, arrow);

    }

}
