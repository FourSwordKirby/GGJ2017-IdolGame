using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sparkles : MonoBehaviour {

    public Image FilledSparkle;
    public float timer;

    private float shineTime;

    // Use this for initialization
    void Start () {
        shineTime = Random.Range(4.0f, 9.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if(timer < shineTime)
        {
            timer += Time.deltaTime;
            if(timer >= shineTime)
            {
                timer = 0;
                shineTime = Random.Range(4.0f, 9.0f);
                StartCoroutine(Shine());
            }
        }
    }

    public IEnumerator Shine()
    {
        float sizeTimer = 0;
        float beatLength = 0.3f;
        while (sizeTimer < 10 * beatLength)
        {
            sizeTimer += Time.deltaTime;
            if (sizeTimer < beatLength)
            {
                this.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 179), sizeTimer/beatLength);
                FilledSparkle.color = Color.Lerp(Color.white-Color.black, Color.white/2, sizeTimer / beatLength);
                yield return new WaitForSeconds(0.01f);
            }
            else if (sizeTimer < 2 * beatLength)
            {
                this.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 181), Quaternion.Euler(0, 0, 0), (sizeTimer-beatLength)/beatLength);
                FilledSparkle.color = Color.Lerp(Color.white / 2, Color.white, (sizeTimer-beatLength) / beatLength);
                yield return new WaitForSeconds(0.01f);
            }
            else 
            {
                if (sizeTimer > 5 * beatLength)
                    FilledSparkle.color = Color.Lerp(Color.white, Color.white - Color.black, (sizeTimer - 5 * beatLength) / (5 * beatLength));
                yield return new WaitForSeconds(0.01f);
            }
        }

        yield return null;
    }
}
