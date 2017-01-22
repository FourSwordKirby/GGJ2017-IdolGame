using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteColor : MonoBehaviour {

    public Gradient color;
    private SpriteRenderer s;

	// Use this for initialization
	void Start () {
        s = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        s.color = color.Evaluate(Time.time % 1);
	}
}
