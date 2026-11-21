using UnityEngine;
using UnityEngine.EventSystems; // Librería obligatoria para detectar el mouse

public class SonidosUI : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [Header("Archivos de Sonido")]
    [SerializeField] private AudioClip sonidoHover;
    [SerializeField] private AudioClip sonidoClick;

    // Esta función se dispara automáticamente al PASAR el mouse por encima
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance != null && GameManager.Instance.reproductorAudio != null && sonidoHover != null)
        {
            GameManager.Instance.reproductorAudio.PlayOneShot(sonidoHover);
        }
    }

    // Esta función se dispara automáticamente al HACER CLIC
    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance != null && GameManager.Instance.reproductorAudio != null && sonidoClick != null)
        {
            GameManager.Instance.reproductorAudio.PlayOneShot(sonidoClick);
        }
    }
}