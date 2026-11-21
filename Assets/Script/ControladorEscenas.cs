using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEscenas : MonoBehaviour
{
    // Carga la pantalla de selección de nivel
    public void IrASeleccionarNivel()
    {
        // NUEVO: Borra los datos de partidas anteriores para empezar de cero
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene("SeleccionNiveles");
    }

    // Regresa a la portada / menú principal
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Método flexible por si prefieres escribir el nombre directamente en el inspector
    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
        PlayerPrefs.DeleteAll();
    }
}