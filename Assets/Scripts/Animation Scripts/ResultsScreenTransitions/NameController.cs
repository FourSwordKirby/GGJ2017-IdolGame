using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NameController : MonoBehaviour {
    public InputField inputField;

    // Update is called once per frame
    void Update () {
        inputField.text = inputField.text.Replace(" ", "");
		if(inputField.text.Length > 20)
            inputField.text = inputField.text.Substring(0, 20);
    }

    public void AppendEmoji(string emoji)
    {
        inputField.text += emoji;
    }

    public string GetName()
    {
        return inputField.text;
    }

    public IEnumerator Display()
    {
        //CHANGE THIS LATER
        yield return null;
    }

    public IEnumerator Dismiss()
    {
        //CHANGE THIS LATER
        this.gameObject.SetActive(false);
        yield return null;
    }
}
