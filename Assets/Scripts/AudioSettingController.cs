using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource backgroundAudio = null;
    [SerializeField] Slider backgroundAdjustBar = null;
    [SerializeField] AudioSource effectAudio = null;
    [SerializeField] Slider effectAdjustBar = null;
    public static AudioSettingController Instance = null;

    float backgroundValue = 0.5f;
    float effectValue = 0.5f;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (AudioValue.Instance.GetBackgroundValue() != 0.5f)
        {
            backgroundAdjustBar.value = AudioValue.Instance.GetBackgroundValue();
        }
        if (AudioValue.Instance.GetffectValue() != 0.5f)
        {
            effectAdjustBar.value = AudioValue.Instance.GetffectValue();
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*backgroundAdjustBar.value = AudioValue.Instance.GetBackgroundValue();
        effectAdjustBar.value = AudioValue.Instance.GetffectValue();*/
        backgroundAudio.volume = backgroundAdjustBar.value;
        effectAudio.volume = effectAdjustBar.value;
        backgroundValue = backgroundAudio.volume;
        effectValue = effectAudio.volume;
        AudioValue.Instance.SetBackGroundValue(backgroundValue);
        AudioValue.Instance.SetEffectValue(effectValue);
    }
    public float getBackgroundValue()
    {
        return backgroundValue;
    }
    public float getEffectValue()
    {
        return effectValue;
    }
    public void setBackGroundValue(float value)
    {
        backgroundValue = value;
    }
    public void setEffectGroundValue(float value)
    {
        effectValue = value;
    }
}
