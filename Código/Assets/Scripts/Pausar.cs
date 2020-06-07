using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pausar : MonoBehaviour
{
    Canvas canvas;

    public AudioMixer mixer;
    public Slider sliderMusica;
    public Slider sliderEfectos;
         
    void Start()
    {
         cargarEstado();
    }

    public void pausa()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }

        guardarEstado();
    }

    public void menu()
    {
        SceneManager.LoadScene(1);
    }

    void cargarEstado()
    {
        //para que cambie de valor y se lo asigne
        sliderMusica.value = 0;
        sliderMusica.value = -1;
        sliderEfectos.value = 0;
        sliderEfectos.value = -1;

        sliderMusica.value = DatosAudio.musica; 
        sliderEfectos.value = DatosAudio.efectos; 

       mixer.SetFloat("musicaVolumen", DatosAudio.musica);
       mixer.SetFloat("efectosVolumen", DatosAudio.efectos);
    }

    public void guardarEstado()
    {
        DatosAudio.musica = sliderMusica.value;
        DatosAudio.efectos = sliderEfectos.value;
    }

    public void setVolumenMusica(float volumen)
    {
        mixer.SetFloat("musicaVolumen", volumen);
    }

    public void setVolumenEfectos(float volumen)
    {
        mixer.SetFloat("efectosVolumen", volumen);
    }
}
