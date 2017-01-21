using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSugoi : MonoBehaviour
{
    public GameObject[] letters;
    public int amount;

    public Gradient color;
    public AnimationCurve colorCurve;
    public AnimationCurve yMovement;
    public AnimationCurve scale;

    void Start()
    {
        StartCoroutine(SpawnLetters());
        Destroy(gameObject, 3 + (amount + 6) * 0.1f);
    }

    private IEnumerator SpawnLetters()
    {
        for (int i = 0; i < letters.Length + amount; i++)
        {
            GameObject o = Instantiate(letters[Mathf.Min(i, letters.Length - 1)]);
            o.GetComponent<EffectSugoiLetter>().SetParams(color, colorCurve, yMovement, scale);
            o.transform.parent = transform;
            if (i < 4)
            {
                o.transform.localPosition = Vector3.right * i * 0.22f;
            }
            else
            {
                o.transform.localPosition = Vector3.right * (0.95f + 0.12f * (i - 5));
            }
            o.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
