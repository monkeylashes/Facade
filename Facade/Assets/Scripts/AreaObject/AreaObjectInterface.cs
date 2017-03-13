using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaObjectInterface : MonoBehaviour {
    private enum children
    {
        MainUI
    }

    private bool isSelected = false;

    public GameObject MainUI;
    
    void Start()
    {
        MainUI = transform.GetChild((int)children.MainUI).gameObject;
        UnityEngine.Events.UnityAction deleteAction = () => { Destroy(); };
        MainUI.transform.GetChild(0).FindChild("Delete").GetComponent<Button>().onClick.AddListener(deleteAction);
        
    }
    
    void Destroy()
    {
        // delete top most parent
        DestroyImmediate(transform.root.gameObject);
    }

    public bool IsSelected
    {
        get {
            return isSelected;
        }
        set
        {            
            isSelected = value;
            MainUI.SetActive(value);
        }
    }
}
