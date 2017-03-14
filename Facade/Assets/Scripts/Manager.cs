using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    private List<GameObject> areaObjectsInScene;

    public delegate void SelectionEventHandler(GameObject selectedObject);
    public delegate void ObjectSpawnEventHandler(GameObject newGameObject);

    public static event SelectionEventHandler ObjectSelected;
    public static event SelectionEventHandler ObjectDeselected;
    public static event ObjectSpawnEventHandler NewGameObjectSpawned;
    public static event ObjectSpawnEventHandler NewGameObjectDespawned;

    private string meshPrefabStorePath = "Prefabs/Store/";
    private TextAsset jsonText;
    public static MeshDataCollection meshDataCollection;

    public static void AreaObjectSelected(GameObject areaObject)
    {
        if(ObjectSelected != null)
        {
            ObjectSelected(areaObject);
        }
        
    }

    public static void AreaObjectDeselected(GameObject areaObject)
    {
        if(ObjectDeselected != null)
        {
            ObjectDeselected(areaObject);
        }        
    }

    public static void GameObjectSpawned(GameObject spawnedObject)
    {
        if (NewGameObjectSpawned != null)
        {
            NewGameObjectSpawned(spawnedObject);
        }
    }

    public static void GameObjectDespawned(GameObject spawnedObject)
    {
        if (NewGameObjectDespawned != null)
        {
            NewGameObjectDespawned(spawnedObject);
        }
    }

    //public void SetSelectedAreaObject(GameObject targetAreaObject)
    //{
    //    foreach (GameObject areaOject in areaObjectsInScene)
    //    {
    //        if(areaOject != targetAreaObject)
    //        {

    //        }
    //    }
    //}

    // Use this for initialization
    void Start () {
		jsonText = Resources.Load<TextAsset>(meshPrefabStorePath + "meshMeta");
        meshDataCollection = JsonUtility.FromJson<MeshDataCollection>(jsonText.text);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
