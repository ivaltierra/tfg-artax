using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basija : MonoBehaviour
{

    public GameObject basijaRota;

    //joyas
    public GameObject joyaVerde;
    public GameObject joyaRoja;
    public GameObject joyaAzul;
    public GameObject corazon;
    public int tipo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void destruir() {

        Instantiate(basijaRota, transform.position, transform.rotation);
        switch (tipo)
        {
            case 1:
                Instantiate(joyaVerde, transform.position, transform.rotation);
                break;
            case 2:
                Instantiate(joyaRoja, transform.position, transform.rotation);
                break;
            case 3:
                Instantiate(joyaAzul, transform.position, transform.rotation);
                break;
            case 4:
                Instantiate(corazon, transform.position, transform.rotation);
                break;

        }
        gameObject.SetActive(false);
    }
}
