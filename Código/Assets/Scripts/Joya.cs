using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joya : MonoBehaviour
{
    public int valor;

    void Start()
    {
        StartCoroutine(eliminar());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ArtaxControler personaje = collision.GetComponent<ArtaxControler>();
        if (personaje != null)
        {
            personaje.GetComponent<ArtaxControler>().sumaJoya(valor,gameObject);
        }
    }

    IEnumerator eliminar()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
