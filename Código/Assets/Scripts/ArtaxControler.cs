using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class ArtaxControler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    //Movimiento
    private float moveSpeed;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;

    [Header("Joistick")]
    public Joystick joistick;

    //armas
    private Transform armaActual;
    private Transform filoActual;
    private float rangoArmaActual;
    [Header("Armas")]
    public Transform arma1;
    public Transform arma1_Filo;
    public float rangoArma1;
    public Transform arma2;
    public Transform arma2_Filo;
    public float rangoArma2;

    public int DanioArma1;
    public int DanioArma2;
    public int DanioArmaActual;
    public GameObject particulasArma1;
    public GameObject particulasArma2;
    private GameObject particulasArmaActual;
    public Transform punteroParticulas;

    //ataque
    private bool esAtaque = false;

    //Layers
    [Header("Layers")]
    public LayerMask lyrEnemigos;
    public LayerMask lyrBasijas;

    //vida
    [Header("Vida")]
    public int nVidas;
    public int vida;
    public TMPro.TextMeshProUGUI textoNvidas;
    public Transform corazones;

    //muerte
    public UnityEngine.UI.Image fondoNegro;
    float ValorAlfaDeseado;
    bool esMuerte;

    //Joyas
    [Header("Joyas")]
    public int nJoyas;
    public TMPro.TextMeshProUGUI textoNjoyas;

    //sonido
    [Header("Audios")]
    public AudioSource audioDanio;
    public AudioSource audioCorrer;
    public AudioSource audioSaltar;
    public AudioSource audioAtacar;
    public AudioSource audioMorir;
    public AudioSource audioObjeto;

    //final
    [Header("Texto Final")]
    public GameObject txtFinal;
    float inicioFadeOut = float.MaxValue;
    bool esFinal = false;
    int escenaAcargar;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        moveSpeed = 12f;

        //poner fondo negro
        fondoNegro.color = new Color(0, 0, 0, 1); //negro
        ValorAlfaDeseado = 0; //transparente

        //Arma por defecto
        armaActual = arma1;
        filoActual = arma1_Filo;
        rangoArmaActual = rangoArma1;
        DanioArmaActual = DanioArma1;
        particulasArmaActual = particulasArma1;

        //esMuerte
        esMuerte = false;

        //vidas
        int i = 0;
        foreach (RectTransform corazon in corazones)
        {
            i++;
            corazon.anchoredPosition = new Vector2(i*35, -73);
        }

        //estadoJuego
        if(EstadoJuego.estadoJuego.partidaGuardarda) cargarEstadoJuego();
    }

    
    private void Update()
    {
        //energia <=0 return poner qui desactivar los controles con un return
        if (esMuerte || esFinal) return;

        //ataque
        if (CrossPlatformInputManager.GetButtonDown("Atack"))
        {
            particulasArmaActual.gameObject.SetActive(true);
            audioAtacar.Play();

            ataque();
            
            if (!(anim.GetBool("esSalto") == true || anim.GetBool("esCaida") == true)) //si es salto o caida se sigue deplazando si no el personaje se para
            {
                dirX = 0;
                rb.velocity = new Vector2(dirX, rb.velocity.y);
                esAtaque = true;
            }
        }

        if (esAtaque) return;

        //movimiento del joistick
        if (joistick.Horizontal >= .2f)
        {
            dirX = moveSpeed;
        }
        else if (joistick.Horizontal <= -.2f)
        {
            dirX = -moveSpeed;
        } else
        {
            dirX = 0f;
        }
        dirX = joistick.Horizontal * moveSpeed;


        //correr
        if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
        {
            anim.SetBool("esCorrer", true);
            audioCorrer.Play();
        }
        else
        {
            audioCorrer.Stop();
            anim.SetBool("esCorrer", false);
        }


        //salto
        if (CrossPlatformInputManager.GetButtonDown("Jump") && rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * 1000f);
            audioSaltar.Play();
        }

        //posado en el suelo
        if (rb.velocity.y == 0)
        {
            anim.SetBool("esSalto", false);
            anim.SetBool("esCaida", false);
        }

        //salto ascendente
        if (rb.velocity.y > 0)
        {
            anim.SetBool("esSalto", true);
        }

        //salto descendente
        if (rb.velocity.y < 0)
        {
            anim.SetBool("esSalto", false);
            anim.SetBool("esCaida", true);
        }
    }

    private void FixedUpdate()
    {
        if (esAtaque) return;

        if(!esMuerte && !esFinal) rb.velocity = new Vector2(dirX, rb.velocity.y);

        //fadeIn muerte
        float valorAlfa = Mathf.Lerp(fondoNegro.color.a, ValorAlfaDeseado, .05f);
        fondoNegro.color = new Color(0, 0, 0, valorAlfa);

        //reiniciar escena cuando se complete el fadeOut
        if (valorAlfa > 0.9f && ValorAlfaDeseado == 1)
        {
            if (escenaAcargar > 5)//hasta la iltima parntalla
            {
                //fin de partida o Victoria
                EstadoJuego.estadoJuego.inicializar();
            }
            SceneManager.LoadScene(escenaAcargar);
        }

        //actualizoNvidas
        textoNvidas.text = nVidas.ToString();
        //devolver contador de joyas al valor
        textoNvidas.fontSize = Mathf.Lerp(textoNvidas.fontSize, 40, .1f);

        //actualizoNJoyas
        textoNjoyas.text = nJoyas.ToString();
        //devolver contador de joyas al valor
        textoNjoyas.fontSize = Mathf.Lerp(textoNjoyas.fontSize, 40, .1f);

        //actualiza vidas
        //vidas
        int i = 0;
        Color colorCorazon = Color.white;
        foreach (RectTransform corazon in corazones)
        {
            i++;
            colorCorazon = Color.white;
            if (vida < i) colorCorazon = Color.black;
            corazon.GetChild(0).GetComponent<SpriteRenderer>().color = colorCorazon;
        }

        //paso de nivel?
        if (Time.time > inicioFadeOut) {
            esFinal = true;
            iniciarfadeInOut();
            inicioFadeOut = float.MaxValue;
        } 
    }

    private void LateUpdate()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("plataformaMovil"))
        {
            transform.parent = collision.transform;
            anim.SetBool("esCaida", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("plataformaMovil"))
        {
            transform.parent = null;
            anim.SetBool("esCaida", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        //Gestión cambio de arma
        if (colision.gameObject.CompareTag("arma"))
        {
            //elimino el arma del suelo
            Destroy(colision.gameObject);

            //asigno los valores del nuevo arma
            armaActual = arma2;
            filoActual = arma2_Filo;
            DanioArmaActual = DanioArma2;
            particulasArmaActual = particulasArma2;

            //oculto el arma1
            arma1.gameObject.SetActive(false);

            //muestro el arma2
            arma2.gameObject.SetActive(true);
        }

        //Gestión checkpiont
        if (colision.gameObject.CompareTag("checkpoint"))
        {
            colision.gameObject.GetComponent<Animator>().SetBool("esSeleccion", true);
            EstadoJuego.estadoJuego.posicionPersonaje = colision.gameObject.transform.position;
        }

        if (colision.gameObject.CompareTag("final"))
        {
            //mostrar cartel
            esFinal = true;
            txtFinal.SetActive(true);
            anim.SetBool("esCorrer", false);
            audioCorrer.Stop();
            anim.SetBool("esSalto", false);
            anim.SetBool("esCaida", false);
            inicioFadeOut = Time.time + 3f;
            escenaAcargar = escenaActual() + 1;
        }

        if (colision.gameObject.CompareTag("finalJuego"))
        {
            //mostrar cartel
            esFinal = true;
            txtFinal.SetActive(true);
            anim.SetBool("esCorrer", false);
            audioCorrer.Stop();
            anim.SetBool("esSalto", false);
            anim.SetBool("esCaida", false);
            inicioFadeOut = Time.time + 3f;
            escenaAcargar = 7;
        }
    }

    void ataque()
    {
        //animacián atacar
        anim.SetTrigger("esAtaque");

        // Detectar los enemigos en el rango de ataque
        Collider2D[] hit = Physics2D.OverlapCircleAll(filoActual.position, rangoArmaActual, lyrEnemigos);

        //dañar enemigos golpeados
        foreach (Collider2D enemigo in hit) {
            if (enemigo.GetType().ToString().Equals("UnityEngine.BoxCollider2D"))
            {
                if (enemigo.GetComponent<Enemigo>() != null)
                    enemigo.GetComponent<Enemigo>().recibirDanio(DanioArmaActual);
                if (enemigo.GetComponent<Jar>() != null)
                    enemigo.GetComponent<Jar>().recibirDanio(DanioArmaActual);
            }
                
        }

        //detectar basijas
        Collider2D[] hitBasijas = Physics2D.OverlapCircleAll(filoActual.position, rangoArmaActual, lyrBasijas);

        //dañar basijas
        foreach (Collider2D basija in hitBasijas)
        {
            if (basija.GetType().ToString().Equals("UnityEngine.BoxCollider2D"))
                basija.GetComponent<Basija>().destruir();
        }
    }

    public void recibirAtaque(int danio)
    {
        audioDanio.Play();
        vida -= danio;
        if (vida <= 0) muere();
    }

    public void finalizaAtaque()
    {
        esAtaque = false;
        StartCoroutine(esperarParticulasArma());
    }

    public void muere()
    {
        escenaAcargar = escenaActual();
        nVidas -= 1;
        esMuerte = true;
        audioMorir.Play();
        anim.SetBool("esMuerte", true);

        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().drag = 50;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;

        if (nVidas <= 0)
        {
            escenaAcargar = 6;//fin de partida
            inicioFadeOut = Time.time + 3f;
        } 
    }

    public void iniciarfadeInOut()
    {
        guardarEstadoJuego();
        ValorAlfaDeseado = 1; //fadeOut
    }

    private void OnDrawGizmosSelected()
    {
        if (filoActual == null)
            return;
        Gizmos.DrawWireSphere(filoActual.position, rangoArmaActual);
    }
        
    IEnumerator esperarParticulasArma()
    {
        yield return new WaitForSeconds(0.1f);
        particulasArmaActual.gameObject.SetActive(false);
    }

    public void sumaJoya(int valor, GameObject joya) {
        
        audioObjeto.Play();

        //añadoVariable a nJoyas
        EstadoJuego.estadoJuego.nJoyas += valor;

        if (EstadoJuego.estadoJuego.nJoyas >= 100) {
            EstadoJuego.estadoJuego.nJoyas-=100;
            //sumo vida
            EstadoJuego.estadoJuego.nVidas+= 1;
            nVidas = EstadoJuego.estadoJuego.nVidas;
            textoNvidas.text = nVidas.ToString();
            textoNvidas.fontSize = textoNvidas.fontSize + 10;
        }

        nJoyas = EstadoJuego.estadoJuego.nJoyas;

        //actualizoNjoyas
        String sJoyas = "";
        if (nJoyas > 9) sJoyas = "0";
        textoNjoyas.text = sJoyas + nJoyas.ToString();
        textoNjoyas.fontSize = textoNjoyas.fontSize + 10;

        Destroy(joya);
    }

    public void sumaCorazon(GameObject corazon) {
        audioObjeto.Play();
        if (vida < 3)
            vida += 1;
        Destroy(corazon);
    }

    void guardarEstadoJuego() {
        EstadoJuego.estadoJuego.nVidas = nVidas;
        EstadoJuego.estadoJuego.nJoyas = nJoyas;

        if (esFinal)
        {
            EstadoJuego.estadoJuego.posicionPersonaje = new Vector2();
            EstadoJuego.estadoBasijas.Clear();
            esFinal = false;
        }
        else
        {
            //basijas
            Transform Basijas = GameObject.Find("Basijas").transform;
            EstadoJuego.estadoBasijas.Clear();

            foreach (Transform basija in Basijas)
            {
                EstadoJuego.EstadoBasijas estadoBasija = new EstadoJuego.EstadoBasijas
                {
                    activo = basija.gameObject.activeSelf
                };
                EstadoJuego.estadoBasijas.Add(estadoBasija);
            }
        }

        //Enemigos
        Transform Enemigos = GameObject.Find("Enemigos").transform;
        EstadoJuego.estadoEnemigos.Clear();

        foreach (Transform enemigo in Enemigos)
        {
            EstadoJuego.EstadoEnemigos estadoEnemigo = new EstadoJuego.EstadoEnemigos
            {
                activo = enemigo.gameObject.activeSelf
            };
            EstadoJuego.estadoEnemigos.Add(estadoEnemigo);
        }

        //checkpoints
        Transform CheckPoints = GameObject.Find("CheckPoints").transform;
        EstadoJuego.estadoChecks.Clear();

        foreach (Transform checkPonit in CheckPoints)
        {
            EstadoJuego.EstadoCheck estadoCheck = new EstadoJuego.EstadoCheck
            {
                activo = checkPonit.gameObject.activeSelf//,
               // posicionCheck = checkPonit.position
            };
            EstadoJuego.estadoChecks.Add(estadoCheck);
        }

        EstadoJuego.estadoJuego.partidaGuardarda = true;        
    }

    void cargarEstadoJuego()
    {
        nVidas = EstadoJuego.estadoJuego.nVidas;
        nJoyas = EstadoJuego.estadoJuego.nJoyas;
        int i = 0;

        if (!(EstadoJuego.estadoJuego.posicionPersonaje==new Vector2()))
            transform.position = EstadoJuego.estadoJuego.posicionPersonaje;
        
        //basijas
        if (EstadoJuego.estadoBasijas.Count > 0)
        {
            Transform Basijas = GameObject.Find("Basijas").transform;
            foreach (Transform basija in Basijas)
            {
                basija.gameObject.SetActive(EstadoJuego.estadoBasijas[i].activo);
                i++;
            }
        }

        //Enemigos
        Transform Enemigos = GameObject.Find("Enemigos").transform;
        i = 0;
        foreach (Transform enemigo in Enemigos)
        {
            enemigo.gameObject.SetActive(EstadoJuego.estadoEnemigos[i].activo);
            i++;
        }

        //checkpoints
        Transform CheckPoints = GameObject.Find("CheckPoints").transform;
        i = 0;
        foreach (Transform checkPoint in CheckPoints)
        {
            if (!EstadoJuego.estadoChecks[i].activo) {
                checkPoint.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("esSeleccion", true);
            }
            i++;
        }

    }

    int escenaActual() {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
