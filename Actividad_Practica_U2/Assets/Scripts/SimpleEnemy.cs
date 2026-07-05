using UnityEngine;
public class SimpleEnemy : MonoBehaviour
{
    // Velocidad con la que se moverá el enemigo.
    public float velocidad = 2f;
    // Distancia máxima que recorrerá a la izquierda y derecha
    // desde la posición donde fue colocado en la escena.
    public float distanciaPatrulla = 3f;
    // Referencia al componente Animator del enemigo.
    private Animator animator;
    // Dirección del movimiento.
    // 1 = derecha
    // -1 = izquierda
    private float direccion = 1f;
    // Guardan los límites izquierdo y derecho de la patrulla.
    private float limiteIzquierdo;
    private float limiteDerecho;
    void Start()
    {
        animator = GetComponent<Animator>();
        // Calcula el límite izquierdo tomando la posición inicial
        // y restándole la distancia de patrulla.
        limiteIzquierdo = transform.position.x - distanciaPatrulla;
        // Calcula el límite derecho sumando la distancia de patrulla.
        limiteDerecho = transform.position.x + distanciaPatrulla;
    }
    void Update()
    {
        // Mueve al enemigo en el eje X.
        // Vector2.right representa el vector (1,0).
        // direccion decide si avanza o retrocede.
        // velocidad controla qué tan rápido se mueve.
        // Time.deltaTime hace que el movimiento sea independiente
        // de los FPS del juego.
        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);
        // Si llega al límite derecho...
        if (transform.position.x >= limiteDerecho)
        {
            // Cambia la dirección para volver hacia la izquierda.
            direccion = -1f;
            // Actualiza la orientación del sprite según la dirección.
            ActualizarOrientacion();
        }
        // Si llega al límite izquierdo...
        if (transform.position.x <= limiteIzquierdo)
        {
            // Cambia la dirección para avanzar hacia la derecha.
            direccion = 1f;
            // Actualiza la orientación del sprite según la dirección.
            ActualizarOrientacion();
        }
        // Comprueba que exista un Animator.
        if (animator != null)
        {
            // Envía al Animator el valor del parámetro "Speed".
            // Mathf.Abs convierte cualquier número negativo en positivo.
            // Así siempre se envía una velocidad positiva.
            animator.SetFloat("Speed", Mathf.Abs(velocidad));
        }
    }
    // Método encargado de orientar visualmente el enemigo
    // según la dirección actual de movimiento.
    void ActualizarOrientacion()
    {
        // Guarda la escala actual del objeto.
        Vector3 escala = transform.localScale;
        // Mathf.Abs obtiene el valor positivo de la escala X
        // sin importar el estado anterior del sprite.
        // Luego se multiplica por direccion (1 o -1)
        // para forzar la orientación correcta.
        // Dirección 1  = escala positiva = mira a la derecha.
        // Dirección -1 = escala negativa = mira a la izquierda.
        escala.x = Mathf.Abs(escala.x) * direccion;
        // Aplica la nueva escala al objeto.
        transform.localScale = escala;
    }
}