using UnityEngine;

public class EfectoParallax : MonoBehaviour
{
    [Header("Velocidad del Fondo")]
    [Tooltip("0 = Se mueve con la cámara, 1 = Se queda completamente quieto")]
    [SerializeField] private float multiplicadorParallax;

    private Transform camaraPrincipal;
    private Vector3 posicionAnteriorCamara;

    void Start()
    {
        // Captura la cámara principal automáticamente
        camaraPrincipal = Camera.main.transform;
        posicionAnteriorCamara = camaraPrincipal.position;
    }

    void LateUpdate()
    {
        // Calcula cuánto se movió la cámara desde el último frame
        float movimientoDeltaX = camaraPrincipal.position.x - posicionAnteriorCamara.x;

        // Mueve el fondo en el eje X basándose en el multiplicador
        transform.position += new Vector3(movimientoDeltaX * multiplicadorParallax, 0, 0);

        // Actualiza la posición de la cámara para el siguiente frame
        posicionAnteriorCamara = camaraPrincipal.position;
    }
}