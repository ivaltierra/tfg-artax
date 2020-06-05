using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{

    public GameObject checkPointSeleccionado;

    public void checkpointSeleccionado()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        GameObject seleccionado = Instantiate(checkPointSeleccionado, transform.parent.position, transform.rotation);
        seleccionado.transform.parent = GameObject.Find("CheckPoints").transform;
    }

    public GameObject Seleccionado() {
        return checkPointSeleccionado;
    }
}
