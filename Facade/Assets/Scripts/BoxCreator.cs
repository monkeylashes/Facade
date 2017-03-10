using UnityEngine;

public class BoxCreator : MonoBehaviour {       
    private GameObject box;
    private GameObject boxButtonsCanvas;
    private GameObject mainCam;

    private string boxPrefabResourcePath = "Prefabs/UserObject";
    private string boxButtonsResourcePath = "Prefabs/ButtonsCanvas";

    public void CreateBox(Vector3 position)
    {
        box = (GameObject)Instantiate(Resources.Load(boxPrefabResourcePath));        
        box.transform.position = position;        
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

        box.transform.position = center;
        box.transform.localScale = new Vector3(width, height, depth);
    }

    public void CreateButtons(Vector3 triggerDownLocation, Vector3 currentControllerLocation)
    {        
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        if(mainCam == null)
        {
            Debug.Log("Can't get gameObject with tag MainCamera");
            return;
        }

        Transform closestAnchor = null;
        foreach (Transform child in box.transform)
        {
            if(closestAnchor == null)
            {
                closestAnchor = child;
                continue;
            }

            if(Vector3.Distance(child.position, mainCam.transform.position) < Vector3.Distance(closestAnchor.position, mainCam.transform.position))
            {
                closestAnchor = child;
            }
        }

        if(closestAnchor == null)
        {
            Debug.Log("Can't find closest anchor to the MainCam");
            return;
        }

        boxButtonsCanvas = (GameObject)Instantiate(Resources.Load(boxButtonsResourcePath), closestAnchor.position, closestAnchor.rotation, closestAnchor);        
    }
}
