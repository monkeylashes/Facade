namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UI_Keyboard : MonoBehaviour
    {
        [HideInInspector]
        public InputField input;

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
            Debug.Log("You've typed [" + input.text.ToLower() + "]");

            Attributes attributes = transform.root.GetComponent<Attributes>();
            attributes.Tags.Add(input.text.ToLower());
            input.text = "";
        }

        public void Next()
        {
            // get root transform
            Transform areaObject = transform.root;
            if(areaObject == null)
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
}