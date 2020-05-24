using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    public Animator anim;
    public float velocidad = 7;
    int direccion = 1;
    private Rigidbody2D rb;
  //  private float limiteIzq;
  //  private float limiteDer;
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


    // Start is called before the first frame update
    void Start()
        {
            vidaActual = vida;
    //        rb = GetComponent<Rigidbody2D>();
    //       limiteIzq = transform.position.x - GetComponent<CircleCollider2D>().radius;
    //        limiteDer = transform.position.x + GetComponent<CircleCollider2D>().radius;
    //
            escalaPj = transform.localScale;
        }

    // Update is called once per frame
    void Update()
    {
        //movimiento
        //        rb.velocity = new Vector2(velocidad * direccion, rb.velocity.y);
        //        if (transform.position.x < limiteIzq) direccion = 1;
        //        if (transform.position.x > limiteDer) direccion = -1;
        //        transform.localScale = new Vector3(escalaPj.x * direccion, escalaPj.y, escalaPj.z);

        if (!modoAtaque) {
            seMueveHacia();
        }

        if (!entreLimites() && !enRango && !anim.GetCurrentAnimatorStateInfo(0).IsName("Orco1_Atack")) {
            seleccionarTarjet();
        }

        if (enRango) {
            logicaEnemigo();
        }
    }

    void logicaEnemigo() {
        distanciaEntrePj = Vector2.Distance(transform.position, tarjet.position);
        if (distanciaEntrePj > distanciaAtaque)
        {
            pararAtaque();
        }
        else if (distanciaAtaque >= distanciaEntrePj && enReposo == false) {
            atacar();
        }
        if (enReposo) {
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
        vidaActual -= danio;
        
        anim.SetTrigger("Hurt");

        if (vidaActual <= 0) {
            muere();
        }
    }

    void muere() {

        Debug.Log("Enemigo muerto");
        //Animación de muerte
        esMuerto = true;
        anim.SetBool("esMuerto", true);

        //deshabilitar enemigo
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<BoxCollider2D>().enabled = false;        
        this.enabled = false;
        
    }

    public void enemigodesaparece() {
        Debug.Log("enemigo eliminado -> " + GetComponent<Rigidbody2D>().gameObject.transform.parent.name);
        //Destroy(GetComponent<Rigidbody2D>().gameObject.transform.parent.gameObject);
        GetComponent<Rigidbody2D>().gameObject.transform.parent.gameObject.SetActive(false);

    }

    public void TriggerEnReposo() {
        enReposo = true;
    }

    private bool entreLimites() {
        return transform.position.x > limiteIzq.transform.position.x && transform.position.x < limiteDer.position.x;
    }

    public void seleccionarTarjet() {
        float distanciaIzq = Vector2.Distance(transform.position, limiteIzq.position);
        float distanciaDer = Vector2.Distance(transform.position, limiteDer.position);

        if (distanciaIzq > distanciaDer) {
            tarjet = limiteIzq;
        }
        else {
            tarjet = limiteDer;
        }
        girarPersonaje();
    }

    public void girarPersonaje(){
        if (esMuerto) return;
        //Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > tarjet.position.x){
            direccion = -1;
           // rotation.y = 0;
        }else{
            direccion = 1;
           // rotation.y = 180f;
        }
         transform.localScale = new Vector3(escalaPj.x * direccion, escalaPj.y, escalaPj.z);
         //transform.eulerAngles = rotation;
    }

    public void ataqueInicio()
    {
        enemigoArma.ataque();
    }

    public void ataqueFin()
    {
        enemigoArma.ataqueFin();
    }

    public void mostrarFinalJuego() {
        finalJuego.gameObject.SetActive(true);
    }
}
