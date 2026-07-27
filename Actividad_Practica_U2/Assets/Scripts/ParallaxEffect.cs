using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    // Referencia a la cámara que se utilizará para detectar el movimiento del jugador
    private Camera camara;

    // Controla la intensidad del efecto parallax.
    // Valores bajos hacen que la capa se mueva más despacio que la cámara.
    public float factorParallax = 0.5f;

    // Guarda la posición original de la capa para tomarla como punto de referencia
    private Vector3 posicionInicial;

    // Almacena la posición inicial de la cámara en el eje X
    private float posicionInicialCamara;

    void Start()
    {
        // Obtiene automáticamente la cámara principal de la escena
        camara = Camera.main;

        // Se guardan las posiciones iniciales para calcular el desplazamiento
        // de la cámara sin perder la ubicación original de la capa
        posicionInicial = transform.position;
        posicionInicialCamara = camara.transform.position.x;
    }

    void LateUpdate()
    {
        // Calcula cuánto se ha desplazado la cámara desde que inició la escena
        float desplazamientoCamara = camara.transform.position.x - posicionInicialCamara;

        // Aplica el factor de parallax para que la capa se mueva
        // de forma proporcional al movimiento de la cámara
        float desplazamientoCapa = desplazamientoCamara * factorParallax;

        // Se actualiza únicamente la posición en X.
        // Los ejes Y y Z permanecen iguales para mantener la altura
        // y la profundidad de la capa.
        transform.position = new Vector3(
            posicionInicial.x + desplazamientoCapa,
            transform.position.y,
            transform.position.z
        );
    }
}