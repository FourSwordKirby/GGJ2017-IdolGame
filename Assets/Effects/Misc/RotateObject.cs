using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public float speed;
	
	void Update () {
        Vector3 eulerAngles = transform.localEulerAngles;
        eulerAngles.z += speed * Time.deltaTime;
        transform.localEulerAngles = eulerAngles;
	}
}
