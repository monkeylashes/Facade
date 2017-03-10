using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PositionOut : MonoBehaviour {

    public GameObject textCanvas;
    [HideInInspector]
    public GameObject keyboard;   
    [HideInInspector] 
    public Text textComponent;

	// Use this for initialization
	void Start () {        
        textComponent = textCanvas.GetComponent<Text>();

        keyboard = transform.GetChild(0).gameObject;
        keyboard.SetActive(false);        
    }    
	
	// Update is called once per frame
	void Update () {        
        //textComponent.text = string.Format("x:{0}, y:{1}, z:{2}", transform.position.x, transform.position.y, transform.position.z);
	}
}
