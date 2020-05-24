using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{

    //public Animator anim;
    public GameObject checkPointSeleccionado;

    public void checkpointSeleccionado()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        
        GameObject seleccionado = Instantiate(checkPointSeleccionado, transform.parent.position, transform.rotation);
        seleccionado.transform.parent = GameObject.Find("CheckPoints").transform;
        //Destroy(gameObject.transform.parent.gameObject);
    }

    public GameObject Seleccionado() {
        return checkPointSeleccionado;
    }
}
