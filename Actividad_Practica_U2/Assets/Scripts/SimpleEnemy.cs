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

            // Cambia la orientación del sprite.
            Voltear();
        }

        // Si llega al límite izquierdo...
        if (transform.position.x <= limiteIzquierdo)
        {
            // Cambia la dirección para avanzar hacia la derecha.
            direccion = 1f;

            // Gira nuevamente el sprite.
            Voltear();
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

    // Método encargado de girar visualmente el enemigo.
    void Voltear()
    {
        // Guarda la escala actual del objeto.
        Vector3 escala = transform.localScale;

        // Multiplica la escala en X por -1.
        // Si era 1 pasa a -1.
        // Si era -1 pasa a 1.
        // Esto hace que el sprite mire al lado contrario.
        escala.x *= -1;

        // Aplica la nueva escala al objeto.
        transform.localScale = escala;
    }
}