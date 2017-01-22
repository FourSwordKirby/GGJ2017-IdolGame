using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcertScoreDisplay : MonoBehaviour
{
    public Text ScoreDisplay;

    public AudioClip successSfx;

    public GameObject scoreExplode;
    public GameObject explodeParticles;

    float score;
    float multiplier = 1.0f;
    float targetScore;

    //Call these 2 functions in the concert manager for dank special effects
    public void SetScore(float s)
    {
        float scoreIncrease = s-targetScore;

        this.targetScore = s;

        if(scoreIncrease / multiplier > 1000)
        {
            AudioSource.PlayClipAtPoint(successSfx, Camera.main.transform.position);
            StartCoroutine(Explode(5 + (int)(10 * (scoreIncrease / multiplier - 1000) / 500)));
            StartCoroutine(Explode2(10));
        }
    }

    public void SetMultiplier(float m)
    {
        AudioSource.PlayClipAtPoint(successSfx, Camera.main.transform.position);
        this.multiplier = m;
        StartCoroutine(ConfirmMultiplier());
    }

    //This class used to display the score on the final results screen, do so here
    public void Update()
    {
        ScoreDisplay.text = (int)score + " <size=60>x</size>" + multiplier;
        float prevScore = score;
        score = Mathf.Lerp(score, targetScore, Time.deltaTime);
    }

    private IEnumerator Explode(int intensity)
    {
        for (int i = 0; i < intensity; i++)
        {
            GameObject o = (GameObject)Instantiate(scoreExplode);
            o.GetComponent<Text>().text = (int)score + " <size=60>x</size>" + multiplier;
            o.transform.parent = transform;
            RectTransform r = o.GetComponent<RectTransform>();
            r.localPosition = scoreExplode.GetComponent<RectTransform>().localPosition;
            o.SetActive(true);
            Destroy(o, 2);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator Explode2(int intensity)
    {
        for (int i = 0; i < intensity; i++)
        {
            Instantiate(explodeParticles, new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(10f, 20f)), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ConfirmMultiplier()
    {
        float flashTime = 0.1f;

        float timer = 0.0f;
        while (timer < 3 * flashTime)
        {
            timer += Time.deltaTime;
            if (timer < flashTime)
                ScoreDisplay.color = Color.Lerp(Color.white, Color.white - Color.black, timer / flashTime);
            else
                ScoreDisplay.color = Color.Lerp(Color.white - Color.black, Color.white, (timer - flashTime) / (2 * flashTime));
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }
}
