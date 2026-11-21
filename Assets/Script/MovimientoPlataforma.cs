using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool moviendoDerecha;
    [SerializeField] private LayerMask queEsSuelo; // NUEVO: Para que solo detecte el piso

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 1. Detección de precipicios (hacia abajo)
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia, queEsSuelo);

        // 2. Detección de paredes (hacia adelante)
        // Determinamos la dirección hacia donde mira el enemigo basándonos en la escala local X.
        // Si la escala es positiva, mira a la derecha; si es negativa, a la izquierda.
        Vector2 direccionFrente = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Lanzamos el rayo hacia adelante. La distancia (0.1f) es pequeña para que choque justo cuando toca la pared.
        RaycastHit2D informacionPared = Physics2D.Raycast(controladorSuelo.position, direccionFrente, 0.1f, queEsSuelo);

        // Movimiento constante
        rb.linearVelocity = new Vector2(velocidad, rb.linearVelocity.y);

        // 3. Girar si NO hay suelo (borde) O SI hay una pared bloqueando el paso
        if (informacionSuelo.collider == null || informacionPared.collider != null)
        {
            Girar();
        }
    }

    private void Girar()
    {
        moviendoDerecha = !moviendoDerecha;

        // ACTUALIZADO: Invertimos la escala X igual que en tu script del Jugador
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;

        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        if (controladorSuelo != null)
        {
            // Dibujar el rayo hacia abajo (búsqueda de bordes)
            Gizmos.color = Color.red;
            Gizmos.DrawLine(controladorSuelo.position, controladorSuelo.position + Vector3.down * distancia);

            // Dibujar el rayo hacia adelante (búsqueda de paredes)
            // Se asume que mira a la derecha por defecto en la vista del editor
            Vector3 direccionFrenteVisual = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(controladorSuelo.position, controladorSuelo.position + direccionFrenteVisual * 0.1f);
        }
    }
}