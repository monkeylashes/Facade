using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainUIButtonActions : MonoBehaviour
{

    private InputField input;

    public void ClickKey(string character)
    {
        input.text += character;
    }

    public void Backspace()
    {
        if (input.text.Length > 0)
        {
            input.text = input.text.Substring(0, input.text.Length - 1);
        }
    }

    public void Enter()
    {
        Attributes attributes = transform.root.GetComponent<Attributes>();
        attributes.Tags.Add(input.text.ToLower());
        input.text = "";
        RepopulateScrollView();
    }

    public void Accept()
    {
        Transform areaObject = transform.root;
        if (areaObject == null)
        {
            Debug.Log("Can't get areaObject");
            return;
        }
        areaObject.GetComponent<Controller>().StopUsing(null);
    }

    public void RepopulateScrollView()
    {
        Transform areaObject = transform.root;
        if (areaObject == null)
        {
            Debug.Log("Can't get areaObject");
            return;
        }
        List<string> tags = areaObject.GetComponent<Attributes>().Tags;
        ScrollView scrollView = transform.GetComponentInChildren<ScrollView>();
        scrollView.Clear();
        foreach(string tag in tags)
        {
            scrollView.AddNewLine(tag);
        }
    }

    public void Next()
    {        
        Transform areaObject = transform.root;
        if (areaObject == null)
        {
            Debug.Log("Can't get areaObject");
            return;
        }

        areaObject.GetComponent<Controller>().LoadMatchingMesh();
    }

    private void Start()
    {
        input = GetComponentInChildren<InputField>();
    }
}

