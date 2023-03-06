using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip audio_steaker = null;
    [SerializeField] AudioClip audio_baker = null;
    [SerializeField] AudioClip audio_bar = null;
    [SerializeField] AudioClip audio_chef = null;
    [SerializeField] AudioClip audio_builder = null;
    [SerializeField] AudioClip audio_welcomer = null;
    [SerializeField] AudioClip audio_cleaner = null;

    [SerializeField] AudioSource effectSource = null;
    [SerializeField] AudioSource backgroundSource = null;

    static AudioSource audioSource = null;
    public static AudioController Instance = null;

    void Awake()
    {
        Instance = this;
        audioSource = this.GetComponent<AudioSource>();
        
    }

    void Start()
    {
        if (effectSource != null)
        {
            effectSource.volume = AudioValue.Instance.GetffectValue();
        }
        if (backgroundSource != null)
        {
            backgroundSource.volume = AudioValue.Instance.GetBackgroundValue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setAudio(int which)// steak cake juice chef build cleaner welcomer
    {
        switch (which)
        {
            case 1:
                audioSource.clip = audio_steaker;
                break;
            case 2:
                audioSource.clip = audio_baker;
                break;
            case 3:
                audioSource.clip = audio_bar;
                break;
            case 4:
                audioSource.clip = audio_chef;
                break;
            case 5:
                audioSource.clip = audio_builder;
                break;
            case 6:
                audioSource.clip = audio_cleaner;
                break;
            case 7:
                audioSource.clip = audio_welcomer;
                break;
        }
        audioSource.Play();

    }

    public void PlayClickAudio(GameObject target)
    {
        target.GetComponent<AudioSource>().Play();
    }
    
}
