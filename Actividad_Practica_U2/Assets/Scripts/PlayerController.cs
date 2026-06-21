using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // Velocidad horizontal del jugador
    public float velocidad = 5f;

    // Fuerza con la que el jugador salta
    public float fuerzaSalto = 7f;

    // Distancia del rayo que detecta el suelo
    public float longitudRayo = 0.6f;

    // Capa que Unity reconocerá como "suelo"
    public LayerMask capaSuelo;

    // Referencia al Rigidbody2D del jugador
    // Se usa para controlar la física y movimiento
    private Rigidbody2D rb;

    // Referencia al Animator del jugador
    private Animator animator;

    // Variable que guarda si el jugador está tocando el suelo
    private bool enSuelo;


    //Indica hacia dónde está mirando el personaje.
    private bool mirandoDerecha = true;


    void Start()
    {
        // Obtiene el componente Rigidbody2D del objeto
        // y lo guarda en la variable rb
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }


    void Update()
    {
        // Lee el movimiento horizontal del teclado
        float movimiento = Input.GetAxis("Horizontal");

        // Cambia la velocidad horizontal del jugador
        // Manteniendo la velocidad vertical actual
        rb.linearVelocity = new Vector2(
            movimiento * velocidad,
            rb.linearVelocity.y
        );

        // Verifica si el jugador está tocando el suelo
        enSuelo = EstaEnSuelo();





        // Actualiza los parametros del Animator

        // Velocidad horizontal del personaje
        animator.SetFloat("Speed", Mathf.Abs(movimiento));

        // ¿Está saltando?
        animator.SetBool("IsJumping", !enSuelo);

        // Velocidad vertical actual
        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y);


        // Si está en el suelo y se presiona espacio
        // entonces el jugador salta
        if (enSuelo && Input.GetButtonDown("Jump"))
        {
            Saltar();
        }


        // Voltear el personaje segun direccion
        if (movimiento > 0 && !mirandoDerecha)
        {
            Voltear();  //el jugador se mueve hacia la derecha.
        }
        else if (movimiento < 0 && mirandoDerecha)
        {
            Voltear();  //el jugador se mueve hacia la izquierda.
        }

    }

    // Función que hace saltar al jugador
    void Saltar()
    {
        // Mantiene la velocidad horizontal
        // pero cambia la velocidad vertical
        // para impulsar el salto
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            fuerzaSalto
        );
    }


    void Voltear()
    {
        mirandoDerecha = !mirandoDerecha;         //Cambia el estado de la dirección
        Vector3 escala = transform.localScale;   //Invierte la
        escala.x *= -1;                         // escala en X
        transform.localScale = escala;         //Aplica la nueva escala al personaje
    }



    // Función que detecta si hay suelo debajo del jugador
    bool EstaEnSuelo()
    {

        Vector2 origen = (Vector2)transform.position + Vector2.down * 0.1f;

        // Lanza un rayo hacia abajo desde la posición del jugador
        RaycastHit2D hit = Physics2D.Raycast(
            origen,                // Punto de inicio
            Vector2.down,         // Dirección del rayo
            longitudRayo,         // Distancia del rayo
            capaSuelo             // Qué capas detectar
        );


        // Si el rayo golpea un collider:
        // devuelve true
        // Si no golpea nada:
        // devuelve false
        return hit.collider != null;
    }





}
