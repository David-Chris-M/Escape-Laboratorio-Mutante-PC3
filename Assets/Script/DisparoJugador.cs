using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private Transform controladorDisparo;

    [Header("Ajustes de Disparo")]
    [SerializeField] private float cadenciaDeTiro = 0.5f; // Tiempo mínimo entre disparos
    [SerializeField] private float tiempoParaCargar = 1.5f; // Tiempo para que la bala sea grande

    [Header("Sonidos")]
    [SerializeField] private AudioSource audioJugador;
    [SerializeField] private AudioClip sonidoDisparoNormal;
    [SerializeField] private AudioClip sonidoDisparoCargado;

    [Header("Animación")]
    private Animator animator;

    private float tiempoProximoDisparo;
    private float tiempoBotonPresionado;
    private bool cargandoDisparo;

    private void Update()
    {
        // 1. Iniciar la carga al presionar (si ya pasó la cadencia)
        if (Input.GetButtonDown("Fire1") && Time.time >= tiempoProximoDisparo)
        {
            cargandoDisparo = true;
            tiempoBotonPresionado = Time.time;
        }

        // 2. Disparar al soltar el botón
        if (Input.GetButtonUp("Fire1") && cargandoDisparo)
        {
            float tiempoMantenido = Time.time - tiempoBotonPresionado;

            // Si mantuvo presionado más del tiempo requerido, es un disparo cargado
            bool esDisparoCargado = tiempoMantenido >= tiempoParaCargar;

            Disparar(esDisparoCargado);

            cargandoDisparo = false;
            tiempoProximoDisparo = Time.time + cadenciaDeTiro;
        }
    }

    private void Disparar(bool balaGrande)
    {
        // Creamos la bala
        GameObject proyectil = Instantiate(balaPrefab, controladorDisparo.position, controladorDisparo.rotation);

        // El 'right' del transform detecta automáticamente hacia dónde apunta el guantelete
        Vector2 direccionReal = controladorDisparo.right;

        // Le pasamos esa dirección exacta
        proyectil.GetComponent<Bala>().ConfigurarBala(direccionReal, balaGrande);
        if (animator != null)
        {
            animator.SetTrigger("Atacar");
        }
        // NUEVO: Lógica para los sonidos
        if (audioJugador != null)
        {
            if (balaGrande && sonidoDisparoCargado != null)
            {
                audioJugador.PlayOneShot(sonidoDisparoCargado); // Sonido fuerte
            }
            else if (!balaGrande && sonidoDisparoNormal != null)
            {
                audioJugador.PlayOneShot(sonidoDisparoNormal); // Sonido rápido
            }
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}