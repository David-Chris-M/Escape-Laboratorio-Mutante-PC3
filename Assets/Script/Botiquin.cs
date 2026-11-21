using UnityEngine;

public class Botiquin : MonoBehaviour
{
    [Header("Curación")]
    [SerializeField] private float cantidadCura = 25f; // Cuánta vida recupera

    [Header("Efectos Opcionales")]
    [SerializeField] private AudioClip sonidoCurar;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        // Dentro de OnTriggerEnter2D...
        if (colision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ModificarVidaJugador(cantidadCura);

                // Esta es la línea que reproduce el sonido del botiquín
                if (GameManager.Instance.reproductorAudio != null && sonidoCurar != null)
                {
                    GameManager.Instance.reproductorAudio.PlayOneShot(sonidoCurar);
                }
            }
            Destroy(gameObject);
        }

    }
}