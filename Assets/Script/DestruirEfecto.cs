using UnityEngine;

public class DestruirEfecto : MonoBehaviour
{
    void Start()
    {
        // Destruye el efecto de explosión medio segundo después de aparecer
        Destroy(gameObject, 0.5f);
    }
}