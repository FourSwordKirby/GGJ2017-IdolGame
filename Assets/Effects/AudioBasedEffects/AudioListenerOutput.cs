using UnityEngine;
using System.Collections;

public class AudioListenerOutput : MonoBehaviour {

    public static float[] audioData;
    public static float[] spectrumData = new float[256];


    void Update()
    {
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);
    }

    void OnAudioFilterRead(float[] data, int channels) {
        audioData = data;
    }
}
