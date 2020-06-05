using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCierraPantalla : MonoBehaviour
{

    public GameObject limite;
    public GameObject camara;
    private bool moverCamara = false;
    public AudioSource audioCierre;
    public GameObject txt;

     void Update()
    {
        if (moverCamara)
        {
            Vector3 posicionDeseada = new Vector3(213, -28, -0.3f);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, posicionDeseada, 2.0f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ArtaxControler personaje = collider.GetComponent<ArtaxControler>();
        if (personaje != null)
        {
            limite.SetActive(true);
            audioCierre.Play();
            moverCamara = true;
            Camera.main.GetComponent<cameraFollow>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            txt.SetActive(true);
        }
    }
}
