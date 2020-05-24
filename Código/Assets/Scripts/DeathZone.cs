﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ArtaxControler personaje = collision.gameObject.GetComponent<ArtaxControler>();
        if (personaje != null)
        {
            personaje.GetComponent<ArtaxControler>().muere();
        }
    }
}
