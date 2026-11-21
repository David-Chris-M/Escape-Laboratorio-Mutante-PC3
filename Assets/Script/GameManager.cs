using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Estadísticas Jugador")]
    public int score = 0;
    public int monedas = 0;
    public float vidaJugador = 100f;
    public float vidaMaxima = 100f;

    [Header("Interfaz Jugador")]
    public TextMeshProUGUI textoMonedas;
    public Image barraVidaUI;

    [Header("Interfaz Jefe/Enemigo")]
    public GameObject panelBarraJefe;
    public Image barraVidaJefeUI;
    private float tiempoRestanteVisible;

    public GameObject panelGameOver;

    [Header("Efectos de Sonido")]
    public AudioSource reproductorAudio;
    public AudioClip sonidoGameOver;

    private void Awake()
    {
        // Al usar Prefabs en cada nivel, solo asignamos la instancia para este nivel
        Instance = this;
    }

    private void Start()
    {
        // 1. CARGA LOS DATOS GUARDADOS SI VENIMOS DE OTRO NIVEL
        if (PlayerPrefs.HasKey("PartidaEnCurso"))
        {
            vidaJugador = PlayerPrefs.GetFloat("VidaGuardada");
            monedas = PlayerPrefs.GetInt("MonedasGuardadas");
            score = PlayerPrefs.GetInt("PuntosGuardados");
        }

        // 2. ACTUALIZA LA INTERFAZ INMEDIATAMENTE
        ActualizarUI();
        if (panelBarraJefe != null) panelBarraJefe.SetActive(false);
    }

    private void Update()
    {
        // Oculta la barra del jefe si pasan 5 segundos sin daño
        if (panelBarraJefe != null && panelBarraJefe.activeSelf)
        {
            tiempoRestanteVisible -= Time.deltaTime;
            if (tiempoRestanteVisible <= 0)
            {
                panelBarraJefe.SetActive(false);
            }
        }
    }

    public void AgregarMonedas(int cantidad)
    {
        monedas += cantidad;
        ActualizarUI();
    }

    public void AgregarPuntos(int puntos)
    {
        score += puntos;
    }

    public void ModificarVidaJugador(float cantidad)
    {
        vidaJugador += cantidad;
        vidaJugador = Mathf.Clamp(vidaJugador, 0, vidaMaxima);
        ActualizarUI();

        if (vidaJugador <= 0)
        {
            if (panelGameOver != null && !panelGameOver.activeSelf)
            {
                panelGameOver.SetActive(true);
                Time.timeScale = 0f; // Congela el juego

                if (reproductorAudio != null && sonidoGameOver != null)
                {
                    reproductorAudio.PlayOneShot(sonidoGameOver);
                }
            }
        }
    }

    public void MostrarVidaEnemigo(float vidaActual, float vidaMaxEnemigo)
    {
        if (panelBarraJefe != null && barraVidaJefeUI != null)
        {
            panelBarraJefe.SetActive(true);
            barraVidaJefeUI.fillAmount = vidaActual / vidaMaxEnemigo;
            tiempoRestanteVisible = 5f;
        }
    }

    private void ActualizarUI()
    {
        if (textoMonedas != null) textoMonedas.text = "x " + monedas;
        if (barraVidaUI != null) barraVidaUI.fillAmount = vidaJugador / vidaMaxima;
    }

    public void ReintentarNivel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void SalirAlMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuPrincipal");
    }

    // 3. FUNCIÓN PARA GUARDAR (Llamada desde el Portal)
    public void GuardarDatos()
    {
        PlayerPrefs.SetFloat("VidaGuardada", vidaJugador);
        PlayerPrefs.SetInt("MonedasGuardadas", monedas);
        PlayerPrefs.SetInt("PuntosGuardados", score);
        PlayerPrefs.SetInt("PartidaEnCurso", 1);
    }
}