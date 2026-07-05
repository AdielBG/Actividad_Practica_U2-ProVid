using UnityEngine;

public class ComplexEnemy : MonoBehaviour
{


    // Velocidad a la que se moverá el enemigo.
    public float velocidad = 3f;

    // Distancia máxima para detectar al jugador.
    // Si el jugador entra en este radio, comienza la persecución.
    public float rangoDeteccion = 5f;


    // Referencia al Transform del jugador.
    private Transform jugador;

    // Guarda la posición donde apareció el enemigo.
    private Vector2 posicionOriginal;

    // Referencia al componente Animator del enemigo.
    // Permite cambiar entre animaciones.
    private Animator animator;

    // Indica si actualmente el enemigo está persiguiendo al jugador.
    private bool persiguiendo = false;


    void Start()
    {
        // Obtiene el componente Animator del mismo GameObject.
        animator = GetComponent<Animator>();

        // Guarda la posición inicial del enemigo.
        posicionOriginal = transform.position;

        // Busca un objeto con el Tag "Player".
        GameObject objJugador = GameObject.FindWithTag("Player");

        // Si encontró al jugador, guarda su Transform.
        if (objJugador != null)
        {
            jugador = objJugador.transform;
        }
    }


    void Update()
    {
        // Si no existe el jugador, no hace nada.
        if (jugador == null) return;

        // Calcula la distancia entre el enemigo y el jugador.
        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Comprueba si el jugador está dentro del rango de detección.
        if (distancia <= rangoDeteccion)
        {
            // El enemigo comienza la persecución.
            persiguiendo = true;
            PerseguirJugador();
        }
        else
        {
            // El jugador está lejos.
            persiguiendo = false;
        }

        // Si existe un Animator, actualiza sus parámetros.
        if (animator != null)
        {
            // Cambia entre animaciones de reposo y persecución.
            animator.SetBool("IsChasing", persiguiendo);

            // Envía la velocidad al Animator.
            animator.SetFloat("Speed", velocidad);
        }
    }

    void PerseguirJugador()
    {
        // MoveTowards mueve el enemigo poco a poco
        // hacia la posición del jugador.
        transform.position = Vector2.MoveTowards(
            transform.position,
            jugador.position,
            velocidad * Time.deltaTime
        );

        // Cambia la orientación del sprite dependiendo
        // de si el jugador está a la izquierda o derecha.

        if (jugador.position.x < transform.position.x)
        {
            // Mirar hacia la izquierda.
            transform.localScale = new Vector3(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else
        {
            // Mirar hacia la derecha.
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

}