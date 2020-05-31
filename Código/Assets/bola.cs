using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bola : MonoBehaviour
{
    float velocidad = 7f;
    Rigidbody2D rb;

    ArtaxControler tarjet;
    Vector2 direccion;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tarjet = GameObject.FindObjectOfType<ArtaxControler>();
        direccion = (tarjet.transform.position - transform.position).normalized * velocidad;
        rb.velocity = new Vector2(direccion.x, direccion.y);
        Destroy(gameObject, 4f);
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ArtaxControler personaje = collider.GetComponent<ArtaxControler>();
        if (personaje != null)
        {
            if (personaje.GetComponent<ArtaxControler>().vida >= 0)
                personaje.GetComponent<ArtaxControler>().recibirAtaque(1);
            Destroy(gameObject);
        }
    }
}
