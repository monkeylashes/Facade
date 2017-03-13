using UnityEngine;
using System.Collections.Generic;
public class BoxCreator : MonoBehaviour {
    private GameObject container;
    private GameObject box;
    private GameObject boxButtonsCanvas;
    private GameObject mainCam;

    private string boxPrefabResourcePath = "Prefabs/UserObject";
    private string boxButtonsCanvasResourcePath = "Prefabs/ButtonsCanvas";   

    // buttons
    private string boxButton_plus_resourcePath = "Prefabs/Buttons/Plus";

    //private GameObject sceneData;
    private SceneData sceneDataScript;

    void Star()
    {
        GameObject sceneData = GameObject.FindWithTag("SceneData");
        sceneDataScript = sceneData.GetComponent<SceneData>();
    }
 
    public void CreateBox(Vector3 position)
    {
        container = new GameObject("UserAreaContainer");
        container.AddComponent<UserAreaAttributes>();  
        container.transform.position = position;

        box = (GameObject)Instantiate(Resources.Load(boxPrefabResourcePath));        
        box.transform.position = position;
        box.transform.SetParent(container.transform);
        container.GetComponent<UserAreaAttributes>().box = box;
        sceneDataScript.AddToScene(container);
    }

    public void UpdateBox(Vector3 triggerDownLocation, Vector3 currentControllerLocation)
    {
        // center
        Vector3 center = new Vector3((triggerDownLocation.x + currentControllerLocation.x) * 0.5f, currentControllerLocation.y * 0.5f, (triggerDownLocation.z + currentControllerLocation.z) * 0.5f);

        // width : x
        float width = Vector3.Distance(triggerDownLocation, new Vector3(currentControllerLocation.x, triggerDownLocation.y, triggerDownLocation.z));

        // height : y
        float height = currentControllerLocation.y;

        // depth : z
        float depth = Vector3.Distance(triggerDownLocation, new Vector3(triggerDownLocation.x, triggerDownLocation.y, currentControllerLocation.z));

        // always translate the parent
        container.transform.position = center;

        // apply scale changes only to the child component
        box.transform.localScale = new Vector3(width, height, depth);
    }

    public void CreateButtons(Vector3 triggerDownLocation, Vector3 currentControllerLocation)
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        if (mainCam == null)
        {
            Debug.Log("Can't get gameObject with tag MainCamera");
            return;
        }

        Transform closestAnchor = null;
        foreach (Transform child in box.transform)
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

        container.GetComponent<UserAreaAttributes>().FrontAnchor = closestAnchor;


        closestAnchor.localScale = Vector3.one;

        boxButtonsCanvas = (GameObject)Instantiate(Resources.Load(boxButtonsCanvasResourcePath));
        boxButtonsCanvas.transform.position = closestAnchor.transform.position;
        boxButtonsCanvas.transform.rotation = closestAnchor.transform.rotation;        
        boxButtonsCanvas.transform.SetParent(container.transform);
             
        //attach buttons to the canvas.
        //GameObject keyboard = (GameObject)Instantiate(Resources.Load("Prefabs/WorldKeyboard"), Vector3.zero, Quaternion.identity, boxButtonsCanvas.transform);
        //keyboard.transform.rotation = new Quaternion(keyboard.transform.rotation.x, boxButtonsCanvas.transform.rotation.y, keyboard.transform.rotation.z, boxButtonsCanvas.transform.rotation.w);
        GameObject plusButton = (GameObject)Instantiate(Resources.Load(boxButton_plus_resourcePath), Vector3.zero, boxButtonsCanvas.transform.rotation, boxButtonsCanvas.transform);
        plusButton.transform.localPosition = new Vector3(0, box.transform.localScale.y * 0.5f, 0);
        //keyboard.transform.localPosition = new Vector3(0, box.transform.localScale.y * 0.25f, 0);
    }
}
