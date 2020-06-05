using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraVida : MonoBehaviour
{
    public Slider slider;

    public void asignaValorMaximo(int vida) {
        slider.maxValue = vida;
        slider.value = vida;
    }

    public void asignaVida(int vida) {
        slider.value = vida;
    }
}
