using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad = 15f;
    [SerializeField] private float daño = 20f;
    [SerializeField] private float tiempoDeVida = 3f;

    private Rigidbody2D rb;
    private Vector2 direccionAvance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, tiempoDeVida);
    }

    public void ConfigurarBala(Vector2 direccion, bool esGrande)
    {
        direccionAvance = direccion;

        // Volteamos el sprite del disparo si va hacia la izquierda
        if (direccion.x < 0)
        {
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }

        if (esGrande)
        {
            transform.localScale *= 2f;
            daño *= 2f;
        }
    }

    private void FixedUpdate()
    {
        // En Unity 6 usamos linearVelocity
        rb.linearVelocity = direccionAvance * velocidad;
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        // 1. Si choca con el enemigo, le hace daño y se destruye
        if (colision.CompareTag("Enemigo"))
        {
            Enemigo scriptEnemigo = colision.GetComponent<Enemigo>();
            if (scriptEnemigo != null)
            {
                scriptEnemigo.TomarDaño(daño);
            }
            Destroy(gameObject);
        }
        // 2. Si choca con CUALQUIER OTRA COSA que NO sea el Jugador
        else if (!colision.CompareTag("Player") && !colision.CompareTag("Untagged"))
        {
            // Esto asegura que rebote contra el Tilemap, paredes, cajas, etc.
            Destroy(gameObject);
        }
    }
}