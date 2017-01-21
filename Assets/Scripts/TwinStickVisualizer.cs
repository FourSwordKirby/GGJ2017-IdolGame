using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinStickVisualizer : MonoBehaviour {

    public TwinStickControls controls;
    private Animator anim;

	// Use this for initialization
	void Start () {
		if (!controls)
        {
            controls = GameObject.FindObjectOfType<TwinStickControls>();
            if(!controls)
            {
                Debug.LogError("Can't find TwinStick controls");
            }
        }

        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
