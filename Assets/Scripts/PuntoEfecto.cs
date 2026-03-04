using UnityEngine;

public class EfectoPuntos : MonoBehaviour
{
    public float velocidadSubida = 1f;

    void Update()
    {
        // Esto hace que el número suba constantemente hacia el cielo
        transform.Translate(Vector3.up * velocidadSubida * Time.deltaTime, Space.World);
    }
}