using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaObjectInterface : MonoBehaviour {
    private enum children
    {
        MainUI
    }

    private InputField inputField;

    private UnityEngine.Events.UnityAction deleteAction;
    

    private bool isSelected = false;

    [HideInInspector]
    public GameObject MainUI;
    
    void Start()
    {        
        deleteAction = () => { Destroy(); };
    }
    
    void Destroy()
    {
        // delete top most parent
        Destroy(transform.root.gameObject, .1f);
    }

    public bool IsSelected
    {
        get {
            return isSelected;
        }
        set
        {            
            if(value == false)
            {
                Destroy(MainUI, .1f);
            }else
            {                
                MainUI = (GameObject)Instantiate(Resources.Load("Prefabs/MainUI"), transform, false);
                //MainUI.transform.rotation = transform.rotation;
                //MainUI.transform.SetParent(transform);
                MainUI.transform.FindChild("ButtonsCanvas").FindChild("Delete").GetComponent<Button>().onClick.AddListener(deleteAction);
            }

            isSelected = value;

            if(MainUI != null)
            {
                MainUI.SetActive(value);
            }            
        }
    }
}
