using UnityEngine;
using System.Collections;

public class SpectrumAnalyzer : MonoBehaviour {
    private float _step = 22050 / 1024;

    private AudioSource _bluesource;
    private AudioSource _redsource;
    private AudioSource _greensource;

    public static float[] _lastBeatsTime = new float[2];
    float[] spectrum = new float[1024];
    // C1 to C10
    public static float[] analyzedspectrum = new float[10];
    // used for check dynamics
    public static float[] lastanalyzedspectrum = new float[10];
    // Use this for initialization
    void Start () {
        // Adding music sources
        _bluesource = gameObject.GetComponents<AudioSource>()[0];
        _redsource = gameObject.GetComponents<AudioSource>()[1];
        _greensource = gameObject.GetComponents<AudioSource>()[2];
    }
    // Update is called once per frame
    void Update () {
        updateSpectrum();
        analyzedspectrum.CopyTo(lastanalyzedspectrum,0);
        analyzespectrum();
        if (isThisABeat())
        {
            _lastBeatsTime[1] = _lastBeatsTime[0];
            _lastBeatsTime[0] = Time.time;
        }
    }
    public static float timeSinceLastBeat(int beat)
    {
        return Time.time - _lastBeatsTime[beat];
    }
    void updateSpectrum()
    {
        if (_greensource.mute == false)
        {
            _greensource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        }
        else if (_redsource.mute == false)
        {
            _redsource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        }
        else
        {
            _bluesource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        }
    }
    float getFrequenciesFactor(int lowfreq,int highfreq)
    {
        int low = (int)(lowfreq / _step);
        int high = (int)(highfreq / _step);
        float factor = 0;
        for (int i =low;i<high;i++)
        {
            factor += spectrum[i];
        }
        return factor;
    }
    void analyzespectrum()
    {
        analyzedspectrum[0] = getC1();
        analyzedspectrum[1] = getC2();
        analyzedspectrum[2] = getC3();
        analyzedspectrum[3] = getC4();
        analyzedspectrum[4] = getC5();
        analyzedspectrum[5] = getC6();
        analyzedspectrum[6] = getC7();
        analyzedspectrum[7] = getC8();
        analyzedspectrum[8] = getC9();
        analyzedspectrum[9] = getC10();
    }
    float getC1()
    {
        return getFrequenciesFactor(0,32);
    }
    float getC2()
    {
        return getFrequenciesFactor(32, 66);
    }
    float getC3()
    {
        return getFrequenciesFactor(66, 131);
    }
    float getC4()
    {
        return getFrequenciesFactor(131, 262);
    }
    float getC5()
    {
        return getFrequenciesFactor(262, 523);
    }
    float getC6()
    {
        return getFrequenciesFactor(523, 1047);
    }
    float getC7()
    {
        return getFrequenciesFactor(1047, 2093);
    }
    float getC8()
    {
        return getFrequenciesFactor(2093, 4186);
    }
    float getC9()
    {
        return getFrequenciesFactor(4186, 8372);
    }
    float getC10()
    {
        return getFrequenciesFactor(8372, 16744);
    }
    public float getIntensityLevel()
    {
        float sum = 0;
        for (int i=0;i<analyzedspectrum.Length;i++)
        {
            sum += analyzedspectrum[i];
        }
        return sum;
    }
    public static bool isThisABeat()
    {
        return (analyzedspectrum[2]) - (lastanalyzedspectrum[2]) >0.2;
    }
}
