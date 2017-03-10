using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class BoxDraw : MonoBehaviour {
    [HideInInspector]
    public VRTK_ControllerEvents controller;
    private Text textComponent;
    private bool startDrawing = false;
    private GameObject userObject;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 currentPoint;

    // Use this for initialization
    void Start()
    {
        textComponent = GetComponentInParent<PositionOut>().textComponent;
        SetEventHandlers();
    }

    protected virtual void SetEventHandlers()
    {
        if (controller == null)
        {
            controller = GetComponentInParent<VRTK_ControllerEvents>();
        }

        // menu button
        controller.SubscribeToButtonAliasEvent(VRTK_ControllerEvents.ButtonAlias.TriggerClick, true, TriggerClickStart);
        controller.SubscribeToButtonAliasEvent(VRTK_ControllerEvents.ButtonAlias.TriggerClick, false, TriggerClickEnd);

    }

    protected void TriggerClickStart(object sender, ControllerInteractionEventArgs e)
    {        
        if(textComponent != null)
        {
            textComponent.text = string.Format("Trigger Click Started at position {0},{1},{2}!", transform.position.x, transform.position.y, transform.position.z);
        }

        // instantiate a box prefab and scale it to near 0 at z and x axis while setting the y to controller height.
        userObject = (GameObject)Instantiate(Resources.Load("Prefabs/UserObject"));
        //userObject.transform.localScale = new Vector3(.1f, transform.position.y, .1f);

        // position on the y axis down by half the height
        //userObject.transform.position = new Vector3(transform.position.x, transform.position.y*0.5f, transform.position.z);
        startPoint = transform.position;
        startDrawing = true;
                
    }

    protected void TriggerClickEnd(object sender, ControllerInteractionEventArgs e)
    {
        if (textComponent != null)
        {
            textComponent.text = string.Format("Trigger Click Ended at position {0},{1},{2}!", transform.position.x, transform.position.y, transform.position.z);
        }

        startDrawing = false;

        // add buttons to the userObject
        GameObject buttonCanvas = (GameObject)Instantiate(Resources.Load("Prefabs/ButtonCanvas"));
        buttonCanvas.transform.position = new Vector3(startPoint.x, currentPoint.y, startPoint.z);
        GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        buttonCanvas.transform.LookAt(new Vector3(buttonCanvas.transform.position.x, buttonCanvas.transform.position.y, mainCam.transform.position.z));
        buttonCanvas.transform.SetParent(userObject.transform);
    }

    void FixedUpdate()
    {
        if (!startDrawing)
        {
            return;
        }

        currentPoint = transform.position;

        // center
        //Vector3 center = new Vector3(startPoint.x + currentPoint.x * 0.5f, currentPoint.y * 0.5f, startPoint.z + currentPoint.z + 0.5f);
        //float distance = Vector3.Distance(startPoint, currentPoint);

        Vector3 center = new Vector3((startPoint.x + currentPoint.x) * 0.5f, currentPoint.y * 0.5f, (startPoint.z + currentPoint.z) * 0.5f);

        // width : x
        float width = Vector3.Distance(startPoint, new Vector3(currentPoint.x, startPoint.y, startPoint.z));

        // height : y
        float height = currentPoint.y;

        // depth : z
        float depth = Vector3.Distance(startPoint, new Vector3(startPoint.x, startPoint.y, currentPoint.z));

        // center
        //Vector3 center = new Vector3(width * 0.5f, height * 0.5f, depth * 0.5f);

        userObject.transform.position = center;
        userObject.transform.localScale = new Vector3(width, height, depth);
    }

    void Update()
    {
    
    }
}


