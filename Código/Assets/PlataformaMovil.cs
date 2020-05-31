using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform tarjet;
    public float velocidad;
    private Vector3 inicio, final;
    // Start is called before the first frame update
    void Start()
    {
        tarjet.parent = null;
        inicio = transform.position;
        final = tarjet.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,tarjet.position,velocidad * Time.deltaTime);

        if (transform.position == tarjet.position) {
            tarjet.position = (tarjet.position == inicio) ? final : inicio;
        }
    }
}
