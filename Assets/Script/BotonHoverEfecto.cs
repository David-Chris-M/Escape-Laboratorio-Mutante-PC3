using UnityEngine;
using UnityEngine.EventSystems;

public class BotonHoverEfecto : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Escala")]
    [SerializeField] private Vector3 escalaHover = new Vector3(1.1f, 1.1f, 1f);
    [SerializeField] private float velocidadEscala = 12f;

    private Vector3 escalaOriginal;
    private Vector3 escalaObjetivo;

    private void Awake()
    {
        escalaOriginal = transform.localScale;
        escalaObjetivo = escalaOriginal;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, escalaObjetivo, Time.unscaledDeltaTime * velocidadEscala);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        escalaObjetivo = escalaHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        escalaObjetivo = escalaOriginal;
    }

    private void OnDisable()
    {
        transform.localScale = escalaOriginal;
    }
}