using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUI : MonoBehaviour {

    public Animator anim;
    public GameObject Sugoi;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reward()
    {
        anim.SetTrigger("Reward");
    }

    public void Advance()
    {
        Transform t = Camera.main.transform;
        GameObject g = Instantiate(Sugoi, t, false);
        g.transform.position += g.transform.forward * 2.0f;
        g.transform.position += g.transform.right * -1.0f;
    }
}
