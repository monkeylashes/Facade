using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_ControllerEvents), typeof(BoxCreator))]
public class ControllerInputHandler : MonoBehaviour
{

    private VRTK_ControllerEvents controller;

    //private BoxCreator box;
    private Controller areaObjectController;

    [HideInInspector]
    public bool triggerDown = false;

    [HideInInspector]
    public Vector3 triggerDownLocation;

    [HideInInspector]
    public Vector3 triggerUpLocation;

    [HideInInspector]
    public Vector3 currentControllerLocation;

    public GameObject AreaObjectPrefab;

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
        // don't register this if the user is holding down the touchpad. In that case the trigger click is used for interating with menu objects.
        if (controller.touchpadPressed)
        {
            return;
        }

        triggerDown = true;
        triggerDownLocation = transform.position;
        currentControllerLocation = transform.position;  

        GameObject areaObject = (GameObject)Instantiate(AreaObjectPrefab, triggerDownLocation, Quaternion.identity);
        areaObjectController = areaObject.GetComponent<Controller>();
    }

    private void TriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        triggerDown = false;
        triggerUpLocation = this.transform.position;
        currentControllerLocation = transform.position;        
    }

    void Update()
    {
        if (!triggerDown)
        {
            return;
        }

        if (areaObjectController == null)
        {
            Debug.LogError("areaObjectController is not attached to the gameObject");
            return;
        }

        currentControllerLocation = transform.position;        

        areaObjectController.UpdateScale(triggerDownLocation, currentControllerLocation);
    }
}
