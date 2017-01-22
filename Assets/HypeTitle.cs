﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HypeTitle : MonoBehaviour {
    public Gradient color;
    private Image image;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        image.color = color.Evaluate(Time.time %1);
	}
}
