using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corazon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ArtaxControler personaje = collision.GetComponent<ArtaxControler>();
        if (personaje != null)
        {
            personaje.GetComponent<ArtaxControler>().sumaCorazon(gameObject);
        }
    }
}
