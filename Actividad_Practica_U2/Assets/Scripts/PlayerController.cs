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

    // Variable que guarda si el jugador está tocando el suelo
    private bool enSuelo;


    void Start()
    {
        // Obtiene el componente Rigidbody2D del objeto
        // y lo guarda en la variable rb
        rb = GetComponent<Rigidbody2D>();
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

        // Si está en el suelo y se presiona espacio
        // entonces el jugador salta
        if (enSuelo && Input.GetButtonDown("Jump"))
        {
            Saltar();
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

    // Función que detecta si hay suelo debajo del jugador
    bool EstaEnSuelo()
    {
        // Lanza un rayo hacia abajo desde la posición del jugador
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,   // Punto de inicio
            Vector2.down,         // Dirección del rayo
            longitudRayo,         // Distancia del rayo
            capaSuelo             // Qué capas detectar
        );

        // Dibuja el rayo en rojo en la escena
        // Solo sirve para visualizarlo mientras pruebas el juego
        Debug.DrawRay(
            transform.position,
            Vector2.down * longitudRayo,
            Color.red
        );

        // Si el rayo golpea un collider:
        // devuelve true
        // Si no golpea nada:
        // devuelve false
        return hit.collider != null;
    }

    
}
