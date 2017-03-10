using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_ControllerEvents), typeof(BoxCreator))]
public class ControllerInputHandler : MonoBehaviour
{

    private VRTK_ControllerEvents controller;

    private BoxCreator box;

    [HideInInspector]
    public bool triggerDown = false;

    [HideInInspector]
    public Vector3 triggerDownLocation;

    [HideInInspector]
    public Vector3 triggerUpLocation;

    [HideInInspector]
    public Vector3 currentControllerLocation;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<VRTK_ControllerEvents>();

        if (controller == null)
        {
            Debug.LogError("RightControllerInputHandler is required to be attached to an object with VRTK_ControllerEvents component");
            return;
        }

        controller.TriggerClicked += TriggerClicked;
        controller.TriggerReleased += TriggerReleased;
    }

    private void TriggerClicked(object sender, ControllerInteractionEventArgs e)
    {
        triggerDown = true;
        triggerDownLocation = transform.position;
        currentControllerLocation = transform.position;

        // Create Box
        box = GetComponent<BoxCreator>();
        box.CreateBox(triggerDownLocation);
    }

    private void TriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        triggerDown = false;
        triggerUpLocation = this.transform.position;
        currentControllerLocation = transform.position;
        box.CreateButtons(triggerDownLocation, currentControllerLocation);
    }

    void FixedUpdate()
    {
        if (!triggerDown)
        {
            return;
        }

        if (box == null)
        {
            Debug.LogError("BoxCreator is not attached to the gameObject");
            return;
        }

        currentControllerLocation = transform.position;        

        box.UpdateBox(triggerDownLocation, currentControllerLocation);
    }
}
