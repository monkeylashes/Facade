using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollView : MonoBehaviour {

    public void Clear()
    {
        gameObject.GetComponent<Text>().text = string.Empty;
    }

    public void AddNewLine(string newLine)
    {
        gameObject.GetComponent<Text>().text += newLine + "\n";
    }
}
