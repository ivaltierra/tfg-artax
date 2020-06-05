using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    public Animator anim;
    public float velocidad = 7;
    int direccion = 1;
    private Rigidbody2D rb;
    private Vector3 escalaPj;

    //vida
    public int vida;
    private int vidaActual;
    private bool esMuerto = false;

    //Ataque
    public float distanciaAtaque;
    public float esperaAtaque;
    [HideInInspector] public Transform tarjet;
    [HideInInspector] public bool enRango;
    public GameObject zonaCombate;
    public GameObject areaTrigger;
    private bool atacando = false;
    //arma
    public GameObject cajaAtaque;
    private Enemigo_Arma enemigoArma;

    private float distanciaEntrePj;
    [HideInInspector] public bool modoAtaque;
    private bool enReposo;
    private float intTimer;

    //Patrullar
    public Transform limiteIzq;
    public Transform limiteDer;

    public Transform finalJuego;

    //sonido
    [Header("Audios")]
    public AudioSource audioDanio;
    public AudioSource audioAtacar;
    public AudioSource audioMorir;


    void Start()
        {
            vidaActual = vida;
            escalaPj = transform.localScale;
        }

    void Update()
    {
        if (!modoAtaque)
        {
            seMueveHacia();
        }

        if (!entreLimites() && !enRango && !anim.GetCurrentAnimatorStateInfo(0).IsName("Orco1_Atack"))
        {
            seleccionarTarjet();
        }

        if (enRango)
        {
            logicaEnemigo();
        }
    }

    void logicaEnemigo()
    {
        distanciaEntrePj = Vector2.Distance(transform.position, tarjet.position);
        if (distanciaEntrePj > distanciaAtaque)
        {
            pararAtaque();
        }
        else if (distanciaAtaque >= distanciaEntrePj && enReposo == false)
        {
            atacar();
        }
        if (enReposo)
        {
            pararEnReposo();
            anim.SetBool("esAtaque", false);

        }
    }

    void Awake() {
        vidaActual = vida;
        escalaPj = transform.localScale;
        enemigoArma = GetComponentInChildren<Enemigo_Arma>();

        seleccionarTarjet();
        intTimer = esperaAtaque;
        anim = GetComponent<Animator>();
    }

    void seMueveHacia() {
        anim.SetBool("esAndando", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Orco1_Atack")) {
            Vector2 posicionTarjet = new Vector2(tarjet.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, posicionTarjet, velocidad * Time.deltaTime);
        }
    }

    void atacar() {
        esperaAtaque = intTimer; //resetea el tiempo de espera cuando el personaje entra en el rango
        modoAtaque = true;
        anim.SetBool("esAndando", false);
        anim.SetBool("esAtaque", true);
    }

    void pararAtaque() {
        enReposo = false;
        modoAtaque = false;
        anim.SetBool("esAtaque", false);
    }

    void pararEnReposo() {
        esperaAtaque -= Time.deltaTime;

        if (esperaAtaque <= 0 && enReposo && modoAtaque) {
            enReposo = false;
            esperaAtaque = intTimer;
        }
    }

    public void recibirDanio(int danio) {
        if (!atacando)
        { 
            vidaActual -= danio;
            audioDanio.Play();
            anim.SetTrigger("Hurt");

            if (vidaActual <= 0) {
                muere();
            }
        }
    }

    void muere() {

        audioMorir.Play();
        //Animación de muerte
        esMuerto = true;
        anim.SetBool("esMuerto", true);

        //deshabilitar enemigo
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<BoxCollider2D>().enabled = false;        
        this.enabled = false;
    }

    public void enemigodesaparece()
    {
        GetComponent<Rigidbody2D>().gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void TriggerEnReposo()
    {
        enReposo = true;
    }

    private bool entreLimites()
    {
        return transform.position.x > limiteIzq.transform.position.x && transform.position.x < limiteDer.position.x;
    }

    public void seleccionarTarjet()
    {
        float distanciaIzq = Vector2.Distance(transform.position, limiteIzq.position);
        float distanciaDer = Vector2.Distance(transform.position, limiteDer.position);

        if (distanciaIzq > distanciaDer)
        {
            tarjet = limiteIzq;
        }
        else
        {
            tarjet = limiteDer;
        }
        girarPersonaje();
    }

    public void girarPersonaje()
    {
        if (esMuerto) return;

        if (transform.position.x > tarjet.position.x)
        {
            direccion = -1;
        }else
        {
            direccion = 1;
        }
        transform.localScale = new Vector3(escalaPj.x * direccion, escalaPj.y, escalaPj.z);
    }

    public void ataqueInicio()
    {
        atacando = true;
        audioAtacar.Play();
        enemigoArma.ataque();
    }

    public void ataqueFin()
    {
        enemigoArma.ataqueFin();
        atacando = false;
    }

    public void mostrarFinalJuego()
    {
        finalJuego.gameObject.SetActive(true);
    }
}
