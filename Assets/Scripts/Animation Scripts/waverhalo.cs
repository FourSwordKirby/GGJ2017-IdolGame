using UnityEngine;
using System.Collections;

public class waverhalo : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        this.GetComponent<Light>().range = Mathf.Clamp(this.GetComponent<Light>().range + Random.Range(-0.2f, 0.2f), 1.5f, 2.0f);
	}
}
