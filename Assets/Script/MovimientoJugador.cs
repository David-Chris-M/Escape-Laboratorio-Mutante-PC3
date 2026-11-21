using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("Movimiento")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDeMovimiento = 10f;
    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento = 0.05f;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto = 15f;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;

    [Header("Animacion")]
    private Animator animator;

    [Header("Sonidos")]
    public AudioSource audioJugador;
    public AudioClip sonidoSalto;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

        animator.SetFloat("VelocidadY", rb2D.linearVelocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);

        animator.SetBool("enSuelo", enSuelo);

        Mover(movimientoHorizontal, salto);
        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector2 velocidadObjetivo = new Vector2(mover, rb2D.linearVelocity.y);
        rb2D.linearVelocity = Vector3.SmoothDamp(rb2D.linearVelocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (enSuelo && saltar)
        {
            enSuelo = false;

            // Reiniciamos la velocidad Y a 0 para anular la inercia de la rampa
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, 0f);

            // Aplicamos el salto
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto), ForceMode2D.Impulse);

            // NUEVO: Reproducir el sonido SOLO cuando realmente salta desde el suelo
            if (audioJugador != null && sonidoSalto != null)
            {
                audioJugador.PlayOneShot(sonidoSalto);
            }
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        if (controladorSuelo != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
        }
    }
}