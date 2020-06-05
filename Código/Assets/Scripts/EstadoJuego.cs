using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoJuego : MonoBehaviour
{
    public static EstadoJuego estadoJuego;

    public bool partidaGuardarda = false;
    public int nVidas = 3;
    public int nJoyas = 0;

    //personaje
    public Vector2 posicionPersonaje;

    //Basijas
    public static List<EstadoBasijas> estadoBasijas = new List<EstadoBasijas>();

    //enemigos
    public static List<EstadoEnemigos> estadoEnemigos = new List<EstadoEnemigos>();

    //checkpoints
    public static List<EstadoCheck> estadoChecks = new List<EstadoCheck>();

    private void Awake()
    {
        if (estadoJuego == null)
        {
            estadoJuego = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (estadoJuego != this)
        {
            Destroy(gameObject);
        }
    }

    //Basijas Gestión
    public class EstadoBasijas
    {
        public bool activo;
    }

    //Basijas Gestión
    public class EstadoEnemigos
    {
        public bool activo;
    }

    //Checkpoints Gestión
    public class EstadoCheck
    {
        public bool activo;
       // public Vector2 posicionCheck;
    }

    public void inicializar()
    {
        partidaGuardarda = false;
        nVidas = 3;
        nJoyas = 0;

        //personaje
        posicionPersonaje = new Vector2();

        //Basijas
        estadoBasijas = new List<EstadoBasijas>();

        //enemigos
        estadoEnemigos = new List<EstadoEnemigos>();

        //checkpoints
        estadoChecks = new List<EstadoCheck>();
    }
}
