using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ConfMenu : MonoBehaviour{

    public AudioMixer audioMixer;

    public void AsignarVolumen(float volumen) {
        audioMixer.SetFloat("VolumenMixer", volumen);

    }
}
