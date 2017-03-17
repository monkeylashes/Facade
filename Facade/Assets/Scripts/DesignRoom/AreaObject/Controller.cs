using System.IO;
using System.Collections.Generic;
using System.Linq;
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
    private IList<MeshData> matches;
    private int currentMatchIndex = 0;
    private GameObject currentlyLoadedMesh;

    //[Tooltip("An array of colliders on the object to ignore when being touched.")]
    //public Collider[] ignoredColliders;

    //List<GameObject> currentIgnoredColliders = new List<GameObject>();

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

    public override void StartTouching(GameObject currentTouchingObject)
    {
        //IgnoreColliders(currentTouchingObject);
        base.StartTouching(currentTouchingObject);
    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);       

        // change our color
        areaMesh.GetComponent<Renderer>().material.color = Color.red;

        IsSelected = true;        

        // emit selected event
        Manager.AreaObjectSelected(gameObject);

        // move and rotate the UI to side closest to the player
        SetRotation();
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
        if(areaMesh == null)
        {
            return;
        }

        // center
        attributes.center = new Vector3((startCorner.x + endCorner.x) * 0.5f, endCorner.y * 0.5f, (startCorner.z + endCorner.z) * 0.5f);

        // width : x
        attributes.width = Vector3.Distance(startCorner, new Vector3(endCorner.x, startCorner.y, startCorner.z));

        // height : y
        attributes.height = endCorner.y;

        // depth : z
        attributes.depth = Vector3.Distance(startCorner, new Vector3(startCorner.x, startCorner.y, endCorner.z));

        // always translate the parent
        transform.position = attributes.center;

        // apply scale changes to the child component (areaMesh)
        areaMesh.transform.localScale = new Vector3(attributes.width, attributes.height, attributes.depth);
        
    }

    //protected virtual void IgnoreColliders(GameObject touchingObject)
    //{
    //    if (ignoredColliders != null && !currentIgnoredColliders.Contains(touchingObject))
    //    {
    //        bool objectIgnored = false;
    //        Collider[] touchingColliders = touchingObject.GetComponentsInChildren<Collider>();
    //        for (int i = 0; i < ignoredColliders.Length; i++)
    //        {
    //            for (int j = 0; j < touchingColliders.Length; j++)
    //            {
    //                Physics.IgnoreCollision(touchingColliders[j], ignoredColliders[i]);
    //                objectIgnored = true;
    //            }
    //        }

    //        if (objectIgnored)
    //        {
    //            currentIgnoredColliders.Add(touchingObject);
    //        }
    //    }
    //}

    ///// <summary>
    ///// The ResetIgnoredColliders method is used to clear any stored ignored colliders in case the `Ignored Colliders` array parameter is changed at runtime. This needs to be called manually if changes are made at runtime.
    ///// </summary>
    //public void ResetIgnoredColliders()
    //{
    //    currentIgnoredColliders.Clear();
    //}

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
        GameObject origin = new GameObject();
        origin.transform.position = attributes.center;
        origin.transform.LookAt(closestAnchor);
        attributes.rotation = origin.transform.rotation;
        Destroy(origin);

        dynamicMesh.transform.rotation = attributes.rotation;
    }

    public void ShowInterface()
    {
        IsSelected = true;
    }

    public void HideInterface()
    {
        IsSelected = false;
    }

    private void LoadNextMesh()
    {
        areaMesh.SetActive(false);

        if(currentMatchIndex == matches.Count)
        {
            if (currentlyLoadedMesh != null)
            {
                DestroyImmediate(currentlyLoadedMesh);
            }
            areaMesh.SetActive(true);
            currentMatchIndex = 0;

        }
        else
        {
            MeshData meshMetaData = matches[currentMatchIndex];

            if (currentlyLoadedMesh != null)
            {
                DestroyImmediate(currentlyLoadedMesh);
            }

            GameObject obj = Resources.Load<GameObject>(meshMetaData.prefabPath);

            if(obj == null)
            {
                currentMatchIndex++;                                
            }
            else
            {
                currentlyLoadedMesh = (GameObject)Instantiate(obj, dynamicMesh.transform.position, dynamicMesh.transform.rotation, dynamicMesh.transform);
                currentMatchIndex++;
            }            
        }       
    }

    public void LoadMatchingMesh()
    {
        // get all matching models
        var possibleMatches = Manager.meshDataCollection.meshes.Where(m => attributes.Tags.Any<string>(t => m.tags.Any<string>(t1 => t1 == t)) && m.dimensionX <= attributes.width && m.dimensionY <= attributes.height && m.dimensionZ <= attributes.depth);
        matches = possibleMatches.ToList<MeshData>();

        Debug.Log("Ran LoadMatchignMeshes");
        Debug.Log("Mesh Size: " + matches.Count<MeshData>());
        foreach(MeshData meshData in matches)
        {
            Debug.Log(meshData.name);
        }

        LoadNextMesh();
    }
}
