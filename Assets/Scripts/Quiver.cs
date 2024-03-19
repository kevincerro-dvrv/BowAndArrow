using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRBaseInteractable {
    public GameObject arrowPrefab;

    private XRGrabInteractable spareArrow;
    // Start is called before the first frame update
    void Start() {
        spareArrow = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity).GetComponent<XRGrabInteractable>();
        //Desactivar a spareArrow
        spareArrow.gameObject.GetComponent<Arrow>().autoDisable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        Debug.Log("Quiver.OnSelectEntered");

        if(GameManager.instance.RoundEnded) {
            return;
        }

        base.OnSelectEntered(args);

        //Activar a spareArrow
        spareArrow.gameObject.SetActive(true);
        interactionManager.SelectEnter(args.interactorObject, spareArrow);

        spareArrow = Instantiate(arrowPrefab, args.interactorObject.transform.position, args.interactorObject.transform.rotation).GetComponent<XRGrabInteractable>();
        //Desactivar a spareArrow
        spareArrow.gameObject.GetComponent<Arrow>().autoDisable = true;

    }

}
