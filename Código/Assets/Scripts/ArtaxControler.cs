using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class ArtaxControler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Joystick joistick;
    private float moveSpeed;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;

    //muerte
    public UnityEngine.UI.Image fondoNegro;
    float ValorAlfaDeseado;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        moveSpeed = 15f;

        //poner fondo negro
        fondoNegro.color = new Color(0, 0, 0, 1); //negro
        ValorAlfaDeseado = 0; //transparente

    }

    
    private void Update()
    {
        //energia <=0 return poner qui desactivar los controles con un return

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
        dirX = joistick.Horizontal * moveSpeed;//Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (CrossPlatformInputManager.GetButtonDown("Jump") && rb.velocity.y == 0)
            rb.AddForce(Vector2.up * 1000f);

        if (CrossPlatformInputManager.GetButtonDown("Atack"))
        {
            ataque();
        }

        if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
            anim.SetBool("esCorrer", true);
        else
            anim.SetBool("esCorrer", false);

        if (rb.velocity.y == 0)
        {
            anim.SetBool("esSalto", false);
            anim.SetBool("esCaida", false);
        }

        if (rb.velocity.y > 0)
            anim.SetBool("esSalto", true);

        if (rb.velocity.y < 0) {
            anim.SetBool("esSalto", false);
            anim.SetBool("esCaida", true);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);

        //fadeIn muerte
        float valorAlfa = Mathf.Lerp(fondoNegro.color.a, ValorAlfaDeseado, .05f);
        fondoNegro.color = new Color(0, 0, 0, valorAlfa);

        //reiniciar escena cuando se complete el fadeOut
        if (valorAlfa > 0.9f && ValorAlfaDeseado ==1) SceneManager.LoadScene("Scenes/Pantalla1");
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        var p = collider.gameObject.GetComponent<DeathZone>();
        if (p != null)
        {
            Debug.Log("muerto Artax");
            anim.SetBool("esMuerte", true);
        }
    }

    void ataque()
    {
        //animacián atacar
        anim.SetTrigger("esAtaque");
        // Detectar los enemigos en el rango de ataque
        //infilgir daño a los enemigos
    }

    public void recibirAtaque()
    {

    }

    public void iniciarfadeInOut()
    {
        ValorAlfaDeseado = 1; //fadeOut
    }
}
