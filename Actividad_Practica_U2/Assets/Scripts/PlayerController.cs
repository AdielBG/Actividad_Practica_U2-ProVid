using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


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


    // Referencia al texto del contador en pantalla
    public TextMeshProUGUI textoContador;

    // Diccionario que guarda cuantos objetos
    // de cada tipo hemos recolectado
    private Dictionary<string, int> objetosRecolectados = new Dictionary<string, int>()
    {
        { "Cake", 0 },
        { "Chicken", 0 },
        { "Coffee", 0 },
        { "Jam", 0 },
        { "Cookie", 0 }
    }; // Un diccionario almacena datos en pares: (Clave ? Valor)
       // Nos permite acceder rápidamente a cada contador usando el nombre del objeto.




    void Start()
    {
        // Obtiene el componente Rigidbody2D del objeto
        // y lo guarda en la variable rb
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();


        // Mostramos el contador inicial en pantalla
        ActualizarContador();
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



    // Se ejecuta automaticamente cuando el jugador
    // toca un objeto que tiene Is Trigger activado
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si el objeto tocado tiene el tag Collectible
        if (collision.CompareTag("Collectible"))
        {
            // Obtenemos el nombre del objeto tocado
            string nombreObjeto = collision.gameObject.name;

            // Verificamos que ese nombre existe en el diccionario
            if (objetosRecolectados.ContainsKey(nombreObjeto))
            {
                // Aumentamos en 1 el contador de ese objeto
                objetosRecolectados[nombreObjeto]++;

                // Actualizamos el texto en pantalla
                ActualizarContador();
            }

            // Eliminamos el objeto de la escena
            Destroy(collision.gameObject);
        }
    }

    // Actualiza el texto del contador en pantalla
    void ActualizarContador()
    {
        textoContador.text =
            $"Cake: {objetosRecolectados["Cake"]} | " +
            $"Chicken: {objetosRecolectados["Chicken"]} | " +
            $"Coffee: {objetosRecolectados["Coffee"]} | " +
            $"Jam: {objetosRecolectados["Jam"]} | " +
            $"Cookie: {objetosRecolectados["Cookie"]}";
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


    // Este método es llamado automáticamente por Unity
    // cuando este objeto colisiona físicamente con otro objeto 2D
    // que tenga un Collider2D y un Rigidbody2D.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprueba si el objeto con el que chocó
        // tiene la etiqueta (Tag) "Enemy".
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Reinicia la escena actual.
            // Primero obtiene la escena activa.
            // Luego obtiene su índice (buildIndex).
            // Finalmente vuelve a cargar esa misma escena.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }



}
