using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joya : MonoBehaviour
{
    public int valor;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(eliminar());
    }

    // Update is called once per frame
    void Update()
    {
        
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
