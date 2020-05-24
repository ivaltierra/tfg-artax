using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Arma : MonoBehaviour
{
    public bool ataqueValido = false;
    public int danioInflingido = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ArtaxControler personaje = collider.GetComponent<ArtaxControler>();
        Debug.Log("personaje --> " + collider.gameObject.tag);
        if (personaje != null && ataqueValido) {
            Debug.Log("entro ");
            ataqueValido = false;
            if (personaje.GetComponent<ArtaxControler>().vida>=0)
                personaje.GetComponent<ArtaxControler>().recibirAtaque(danioInflingido);
        }
    }

    public void ataque() {
        ataqueValido = true;
    }

    public void ataqueFin()
    {
        ataqueValido = false;
    }
}
