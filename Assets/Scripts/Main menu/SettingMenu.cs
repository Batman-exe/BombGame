using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    [SerializeField]public Slider slider;
    [SerializeField]public AudioMixer audioMixer;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float volume = -30f;


    private void Awake()
    {
        audioMixer.SetFloat("VolumeSFX", volume);
        slider.value = GetMasterValue();
    }

    public float GetMasterValue()
    {
        float value;
        bool result = audioMixer.GetFloat("VolumeSFX", out value);
        if (result)
        {
            return value;
        } else
        {
            return 0f;
        }
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("VolumeSFX", volume);
    }


    private void OnMouseExit()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(audioClip);
    }


}
