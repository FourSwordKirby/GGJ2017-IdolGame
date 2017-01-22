using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcertCamera : MonoBehaviour {
    /* A bunch of stuff that relates to how the camera shakes*/
    public enum ShakePresets
    {
        NONE,
        BOTH,
        HORIZONTAL,
        VERTICAL
    };
    private float shakeIntensity = 0.0f;
    private float shakeDuration = 0.0f;
    private ShakePresets shakeDirection = ShakePresets.NONE;
    private Vector2 shakeOffset = new Vector2();

    public SpriteRenderer mangaEffect;
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.S))
        //    StartCoroutine(Shake());
        //if (Input.GetKeyDown(KeyCode.W))
        //    StartCoroutine(MangaIntensityEffect());
    }

    private IEnumerator MangaIntensityEffect(float duration = 3f)
    {
        float timer = 0.0f;
        mangaEffect.gameObject.SetActive(true);
        mangaEffect.GetComponent<Rigidbody>().angularVelocity = Vector3.forward * 30.0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            if (timer < 0.2 * duration)
                mangaEffect.transform.localScale = Vector3.Lerp(Vector3.one * 5.0f, Vector3.one * 1.75f, timer / (duration * 0.2f));
            if (timer > 0.8 * duration)
                mangaEffect.transform.localScale = Vector3.Lerp(Vector3.one * 1.75f, Vector3.one * 5.0f, (timer - 0.8f * duration) / (duration * 0.2f));

            yield return new WaitForSeconds(0.01f);
        }
        mangaEffect.gameObject.SetActive(false);
        yield return null;
    }

    private IEnumerator Shake(float Intensity = 1.05f,
                              float Duration = 0.5f,
                              bool Force = true,
                              ShakePresets Direction = ShakePresets.BOTH)
    {
        Vector3 originalPosition = transform.position;

        if (!Force && ((shakeOffset.x != 0) || (shakeOffset.y != 0)))
            yield return null;

        shakeIntensity = Intensity;
        shakeDuration = Duration;
        shakeDirection = Direction;
        shakeOffset.Set(0, 0);

        float timer = 0.0f;
        while(timer < shakeDuration)
        {
            timer += Time.deltaTime;
            if (shakeDirection == ShakePresets.BOTH || shakeDirection == ShakePresets.HORIZONTAL)
                shakeOffset.x = (UnityEngine.Random.Range(-1.0F, 1.0F) * shakeIntensity);
            if (shakeDirection == ShakePresets.BOTH || shakeDirection == ShakePresets.VERTICAL)
                shakeOffset.y = (UnityEngine.Random.Range(-1.0F, 1.0F) * shakeIntensity);

            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;

            transform.position = new Vector3(x + shakeOffset.x, y + shakeOffset.y, z);

            if(timer > shakeDuration * 0.75f)
                transform.position += (originalPosition - transform.position)*0.3f;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = originalPosition;

        shakeOffset.Set(0, 0);
        yield return null;
    }

}
