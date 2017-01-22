using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    public GameObject spectrumBar;

    [Range(0, 255)]
    public int numBars;
    public float minHeight;
    public float maxHeight;
    public Gradient color;

    public bool alignBottom;

    private GameObject[] spectrumBars;

    void Start()
    {
        spectrumBars = new GameObject[numBars];

        for (int i = 0; i < numBars; i++)
        {
            GameObject bar = (GameObject)Instantiate(spectrumBar);
            bar.transform.parent = transform;
            bar.transform.position = Vector3.right * i * 1.5f;
            bar.SetActive(true);

            bar.GetComponent<SpriteRenderer>().color = color.Evaluate(i * 1f / numBars);
            spectrumBars[i] = bar;
        }
    }

    void Update()
    {
        for (int i = 0; i < numBars; i++)
        {
            float height = Mathf.Lerp(minHeight, maxHeight, AudioListenerOutput.spectrumData[i]);
            spectrumBars[i].transform.localScale = new Vector3(1, height, 1);

            if (alignBottom)
            {
                Vector3 v = spectrumBars[i].transform.localPosition;
                v.y = height * 0.5f;
                spectrumBars[i].transform.localPosition = v;
            }
        }
    }
}
