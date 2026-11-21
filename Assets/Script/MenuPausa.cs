using UnityEngine;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private Button botonMenu;
    [SerializeField] private Button botonVolver;

    private void Start()
    {
        panelPausa.SetActive(false); // Oculta el panel al iniciar

        // Conecta los botones a las funciones
        botonMenu.onClick.AddListener(PausarJuego);
        botonVolver.onClick.AddListener(ReanudarJuego);
    }

    public void PausarJuego()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f; // Congela el tiempo del juego
    }

    public void ReanudarJuego()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1f; // Descongela el tiempo
    }
}