using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioSource mixer;
    public Slider slider;

    

   public void Update()
    {
        mixer.volume = slider.value;
    }
}
