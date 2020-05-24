using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_TriggerArea : MonoBehaviour
{
    private Enemigo enemigo;

    private void Awake(){
        enemigo = GetComponentInParent<Enemigo>();
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            gameObject.SetActive(false);
            enemigo.tarjet = collider.transform;
            enemigo.enRango=true;
            enemigo.zonaCombate.SetActive(true);
        }
    }
}
