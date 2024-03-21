using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRBaseInteractable {
    public static Quiver instance;

    public GameObject arrowPrefab;

    private XRGrabInteractable spareArrow;

    private List<GameObject> arrows;

    void Awake() {
        base.Awake();
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        arrows = new List<GameObject>();

        spareArrow = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity).GetComponent<XRGrabInteractable>();
        //Desactivar a spareArrow
        spareArrow.gameObject.GetComponent<Arrow>().autoDisable = true;

        arrows.Add(spareArrow.gameObject);
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

        // Register last delivered arrow
        arrows.Add(spareArrow.gameObject);

        spareArrow = Instantiate(arrowPrefab, args.interactorObject.transform.position, args.interactorObject.transform.rotation).GetComponent<XRGrabInteractable>();
        //Desactivar a spareArrow
        spareArrow.gameObject.GetComponent<Arrow>().autoDisable = true;
    }

    void OnTriggerEnter(Collider other) {
        // Haptic feedback
        other.GetComponent<HapticController>()?.TriggerHaptic();
    }

    public void DestroyArrows() {
        foreach (GameObject arrow in arrows) {
            Destroy(arrow);
        }
        arrows.Clear();
    }
}
