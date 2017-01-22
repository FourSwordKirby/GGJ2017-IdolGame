using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour {

    public GameObject returnButton;

    public IEnumerator Display()
    {
        returnButton.SetActive(false);
        this.gameObject.SetActive(true);
        yield return null;
    }

    public IEnumerator Dismiss()
    {
        //CHANGE THIS LATER
        returnButton.SetActive(true);
        this.gameObject.SetActive(false);
        yield return null;
    }
}
