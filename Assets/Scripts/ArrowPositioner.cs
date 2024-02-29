using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowPositioner : MonoBehaviour
{
    private XRDirectInteractor directInteractor;
    private Arrow selectedArrow = null;

    private Transform stringMiddlePoint = null;

    // Para probas
    public Transform marcador;

    // Start is called before the first frame update
    void Start()
    {
        directInteractor = GetComponent<XRDirectInteractor>();
        directInteractor.selectEntered.AddListener(SelectEntered);
        directInteractor.selectExited.AddListener(SelectExited);
    }

    private void SelectEntered(SelectEnterEventArgs args) {
        if (args.interactable.gameObject.CompareTag("Arrow")) {
            selectedArrow = args.interactable.gameObject.GetComponent<Arrow>();
            selectedArrow.OnStringCaptured += StringCaptured;
        }
    }

    private void SelectExited(SelectExitEventArgs args) {
        selectedArrow = null;
        stringMiddlePoint = null;
        directInteractor.attachTransform.localPosition = Vector3.zero;
        directInteractor.attachTransform.localRotation = Quaternion.identity;
    }

    public void StringCaptured(Transform stringMiddlePoint) {
        this.stringMiddlePoint = stringMiddlePoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (stringMiddlePoint) {
            marcador.position = stringMiddlePoint.position;
            directInteractor.attachTransform.position = stringMiddlePoint.position;
            directInteractor.attachTransform.rotation = stringMiddlePoint.rotation;
        }
    }
}
