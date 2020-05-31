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
     //   canvas = GetComponent<Canvas>();
    //    if(SceneManager.GetActiveScene().buildIndex!=1)
     //       canvas.enabled = false;
         cargarEstado();
    }

    public void pausa()
    {
        // Debug.Log("PASO");
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            
            //canvas.enabled = !canvas.enabled;
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            Debug.Log("paso por pausa");
        }

        guardarEstado();
    }

    public void menu()
    {
        //guardarEstado();
        //EstadoJuego.estadoJuego.inicializar();
        Debug.Log("paso por menu");
        SceneManager.LoadScene(1);
    }

    void cargarEstado() {
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

    void guardarEstado()
    {
        DatosAudio.musica = sliderMusica.value;
        DatosAudio.efectos = sliderEfectos.value;
    }

    public void setVolumenMusica(float volumen) {
        Debug.Log("SET VOLUME");
        mixer.SetFloat("musicaVolumen", volumen);
    }

    public void setVolumenEfectos(float volumen)
    {
        mixer.SetFloat("efectosVolumen", volumen);
    }
}
