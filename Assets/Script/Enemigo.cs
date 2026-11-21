using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Estadísticas")]
    [SerializeField] private float vida = 100f;
    [SerializeField] private float vidaMaxima = 100f; 
    [SerializeField] private int puntosPorDerrota = 20;
    [SerializeField] private float dañoAlJugador = 10f;

    [Header("Efectos")]
    [SerializeField] private GameObject efectoMuerte;

    [Header("Sonidos")]
    [SerializeField] private AudioSource audioSourceEnemigo;
    [SerializeField] private AudioClip sonidoCaminar;
    [SerializeField] private AudioClip sonidoMuerte;
    [SerializeField] private AudioClip sonidoRecibirDaño;
    public void TomarDaño(float daño)
    {
        vida -= daño;
        Debug.Log("Golpeado: " + gameObject.name + " | Vida: " + vida + "/" + vidaMaxima);
        // NUEVO: Reproduce el sonido de recibir daño
        if (audioSourceEnemigo != null && sonidoRecibirDaño != null)
        {
            audioSourceEnemigo.PlayOneShot(sonidoRecibirDaño);
        }

        // Le avisa al GameManager que muestre la barra actualizada
        if (GameManager.Instance != null)
        {
            GameManager.Instance.MostrarVidaEnemigo(vida, vidaMaxima);
        }

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AgregarPuntos(puntosPorDerrota);

            if (GameManager.Instance.reproductorAudio != null && sonidoMuerte != null)
            {
                GameManager.Instance.reproductorAudio.PlayOneShot(sonidoMuerte);
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ModificarVidaJugador(-dañoAlJugador);
            }
        }
    }
    private void Start()
    {
        // 1. Busca su propio Audio Source automáticamente al nacer
        audioSourceEnemigo = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 2. Reproduce el sonido de caminar de forma continua (bucle)
        if (audioSourceEnemigo != null && sonidoCaminar != null)
        {
            if (!audioSourceEnemigo.isPlaying)
            {
                audioSourceEnemigo.clip = sonidoCaminar;
                audioSourceEnemigo.Play(); // Mejor que PlayOneShot para sonidos continuos
            }
        }
    }

}