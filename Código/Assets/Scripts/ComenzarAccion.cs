using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComenzarAccion : MonoBehaviour
{
    public GameObject txt;
    public GameObject pasivo;
    public GameObject activo;
    public barraVida barraVida;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        txt.SetActive(false);
        pasivo.SetActive(false);
        activo.SetActive(true);
        barraVida.gameObject.SetActive(true);
}
}
