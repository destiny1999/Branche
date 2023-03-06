using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioValue : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] static float backgroundValue = 0.5f;
    [SerializeField] static float effectValue = 0.5f;
    //[SerializeField] AudioSource[] audio = null;// effect background
    public static AudioValue Instance;
    void Awake()
    {
        Instance = this;
        AudioSettingController.Instance.setBackGroundValue(backgroundValue);
        AudioSettingController.Instance.setEffectGroundValue(effectValue);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetBackGroundValue(float value)
    {
        backgroundValue = value;
    }
    public void SetEffectValue(float value)
    {
        effectValue = value;
    }
    public float GetBackgroundValue()
    {
        return backgroundValue;
    }
    public float GetffectValue()
    {
        return effectValue;
    }
}
