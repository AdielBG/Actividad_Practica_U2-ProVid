using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Referencia al objeto que la camara seguira
    public Transform objetivo;

    // Qué tan suavemente sigue la camara al jugador
    // Valores más altos = seguimiento más lento y suave
    public float suavizado = 5f;

    // Desplazamiento fijo de la camara respecto al jugador
    public Vector3 desplazamiento = new Vector3(0f, 2f, -10f);

    void LateUpdate()
    {
        if (objetivo == null) return;

        // Calculamos la posicion destino de la camara
        Vector3 posicionDestino = objetivo.position + desplazamiento;

        // Interpolamos suavemente hacia esa posicion
        transform.position = Vector3.Lerp(
            transform.position,
            posicionDestino,
            suavizado * Time.deltaTime
        );
    }
}