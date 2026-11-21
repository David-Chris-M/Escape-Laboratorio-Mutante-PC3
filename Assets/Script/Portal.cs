using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string nombreSiguienteNivel = "Nivel2";
    [Header("Sonido")]
    [SerializeField] private AudioClip sonidoPortal; // NUEVA VARIABLE

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                // NUEVO: Guarda las estadísticas del científico en la memoria
                GameManager.Instance.GuardarDatos();

                if (GameManager.Instance.reproductorAudio != null && sonidoPortal != null)
                {
                    GameManager.Instance.reproductorAudio.PlayOneShot(sonidoPortal);
                }
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(nombreSiguienteNivel);
        }
    }
}