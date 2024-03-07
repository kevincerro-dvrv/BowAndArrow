using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowPositioner : MonoBehaviour {
    private XRDirectInteractor directInteractor;
    Arrow selectedArrow;

    private Transform stringMiddlePoint;

    //Para probas
    public Transform marcador;


    // Start is called before the first frame update
    void Start() {
        directInteractor = GetComponent<XRDirectInteractor>();

        directInteractor.selectEntered.AddListener(SelectEntered);
        directInteractor.selectExited.AddListener(SelectExited);
        selectedArrow = null;
    }

    private void SelectEntered(SelectEnterEventArgs args) {
        Debug.Log("ArrowPositioner collido obxecto" + args.interactable.gameObject.tag);
        if(args.interactable.gameObject.CompareTag("Arrow")) {
            selectedArrow = args.interactable.gameObject.GetComponent<Arrow>();
            selectedArrow.OnStringCaptured += StringCaptured;
        }

    }


    private void StringCaptured(Transform stringMiddlePoint) {
        this.stringMiddlePoint = stringMiddlePoint;        
    }

    private void SelectExited(SelectExitEventArgs args) {
        Debug.Log("ArrowPositioner soltado obxecto" + args.interactable.gameObject.tag);
        selectedArrow = null;
        stringMiddlePoint = null;
        directInteractor.attachTransform.localPosition = Vector3.zero;
        directInteractor.attachTransform.localRotation = Quaternion.identity;

    }

    // Update is called once per frame
    void Update() {
        if(stringMiddlePoint != null) {
            marcador.position = stringMiddlePoint.position;

            directInteractor.attachTransform.position = stringMiddlePoint.position;
            directInteractor.attachTransform.rotation = stringMiddlePoint.rotation;
        }
    }
}
