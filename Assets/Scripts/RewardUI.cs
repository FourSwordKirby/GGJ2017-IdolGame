using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUI : MonoBehaviour {

    public Animator anim;

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
}
