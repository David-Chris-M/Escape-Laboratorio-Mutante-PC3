using UnityEngine;

public class Moneda : MonoBehaviour
{
    [SerializeField] private int valorMoneda = 1;
    [Header("Sonido")]
    [SerializeField] private AudioClip sonidoMoneda; // NUEVA VARIABLE

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AgregarMonedas(valorMoneda);

                // NUEVO: Reproducir sonido de moneda
                if (GameManager.Instance.reproductorAudio != null && sonidoMoneda != null)
                {
                    GameManager.Instance.reproductorAudio.PlayOneShot(sonidoMoneda);
                }
            }
            Destroy(gameObject);
        }
    }
}