using UnityEngine;
using VRTK;

public class Controller : VRTK_InteractableObject
{

    private enum Children
    {
        AreaMesh = 0,
        DynamicMesh,
        UserInterface
    }

    private GameObject areaMesh;
    private GameObject dynamicMesh;
    private GameObject userInterface;
    private AreaObjectInterface userInterfaceScript;
    private Attributes attributes;
    private Color originalColor;
    private GameObject mainCam;

    public bool IsSelected
    {
        get
        {
            return userInterfaceScript.IsSelected;
        }
        set
        {
            userInterfaceScript.IsSelected = value;
        }
    }
    
    void Start()
    {
        areaMesh = transform.GetChild((int)Children.AreaMesh).gameObject;
        dynamicMesh = transform.GetChild((int)Children.DynamicMesh).gameObject;
        userInterface = transform.GetChild((int)Children.UserInterface).gameObject;
        userInterfaceScript = userInterface.GetComponent<AreaObjectInterface>();
        attributes = GetComponent<Attributes>();
        originalColor = areaMesh.GetComponent<Renderer>().material.color;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");        
    }

    private void Destroy()
    {

    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);       

        // change our color
        areaMesh.GetComponent<Renderer>().material.color = Color.red;

        IsSelected = true;

        // move and rotate the UI to side closest to the player
        SetRotation();

        // emit selected event
        Manager.AreaObjectSelected(gameObject);
    }

    public override void StopUsing(GameObject usingObject)
    {
        base.StopUsing(usingObject);
        
        // change color back to original
        areaMesh.GetComponent<Renderer>().material.color = originalColor;

        IsSelected = false;

        // emit deselected event        
        Manager.AreaObjectDeselected(gameObject);
    }

    public void UpdateScale(Vector3 startCorner, Vector3 endCorner)
    {
        // center
        Vector3 center = new Vector3((startCorner.x + endCorner.x) * 0.5f, endCorner.y * 0.5f, (startCorner.z + endCorner.z) * 0.5f);

        // width : x
        float width = Vector3.Distance(startCorner, new Vector3(endCorner.x, startCorner.y, startCorner.z));

        // height : y
        float height = endCorner.y;

        // depth : z
        float depth = Vector3.Distance(startCorner, new Vector3(startCorner.x, startCorner.y, endCorner.z));

        // always translate the parent
        transform.position = center;

        // apply scale changes to the child component (areaMesh)
        areaMesh.transform.localScale = new Vector3(width, height, depth);
    }

    public void SetLocation(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation()
    {        
        if (mainCam == null)
        {
            Debug.Log("Can't get gameObject with tag MainCamera");
            return;
        }

        Transform closestAnchor = null;
        foreach (Transform child in areaMesh.transform)
        {
            if (closestAnchor == null)
            {
                closestAnchor = child;
                continue;
            }

            if (Vector3.Distance(child.position, mainCam.transform.position) < Vector3.Distance(closestAnchor.position, mainCam.transform.position))
            {
                closestAnchor = child;
            }
        }

        if (closestAnchor == null)
        {
            Debug.Log("Can't find closest anchor to the MainCam");
            return;
        }

        userInterface.transform.position = closestAnchor.position;
        userInterface.transform.rotation = closestAnchor.rotation;
    }

    public void ShowInterface()
    {
        IsSelected = true;
    }

    public void HideInterface()
    {
        IsSelected = false;
    }
}
