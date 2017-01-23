using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GesturePrompt : MonoBehaviour {

    private Animator anim;
    public int value;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    public void SetGesture(Gesture g)
    {
        value = (int)g;
    }

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
        if(anim.GetInteger("Gesture") == value)
        {
            anim.SetInteger("Gesture", value);
        }
        anim.SetInteger("Gesture", value);
    }

    public void Enter()
    {
        anim.SetTrigger("Enter");
    }

    public void Die()
    {
        anim.SetTrigger("Die");
        StartCoroutine(ExplodeSelf());
    }

    public IEnumerator ExplodeSelf()
    {
        yield return new WaitForSeconds(.49f);
        Destroy(this.gameObject);
        yield return null;
    }
}
