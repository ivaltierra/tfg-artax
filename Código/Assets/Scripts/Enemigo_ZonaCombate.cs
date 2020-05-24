using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_ZonaCombate : MonoBehaviour
{
    private Enemigo enemigo;  
    private bool enRango;  
    private Animator anim;

    private void Awake(){
        enemigo = GetComponentInParent<Enemigo>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update(){
        if(enRango && !anim.GetCurrentAnimatorStateInfo(0).IsName("Orco1_Atack")) {
            enemigo.girarPersonaje();
        }

    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            enRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            enRango = false;
            gameObject.SetActive(false);
            enemigo.areaTrigger.SetActive(true);
            enemigo.enRango = false;
            enemigo.seleccionarTarjet();
        }
    }
}
