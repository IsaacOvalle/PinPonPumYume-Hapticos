using UnityEngine;
using System.Collections;

public class Topo : MonoBehaviour
{
    private bool fueGolpeado = false;
    private MartilloGolpe martillo;

    [Header("Configuración de Vida")]
    public float tiempoDeVida = 1.5f;

    [Header("Animaciones")]
    public float tiempoEscalado = 0.2f;
    private Vector3 escalaOriginal;

    [Header("Efecto de Advertencia")]
    public Color colorAlerta = Color.red;
    private Color colorOriginal;
    private Renderer rend;

    void Start()
    {
        martillo = FindFirstObjectByType<MartilloGolpe>();
        rend = GetComponentInChildren<Renderer>(); // Busca el render en el modelo

        if (rend != null) colorOriginal = rend.material.color;

        escalaOriginal = transform.localScale;
        StartCoroutine(CicloDeVida());
    }

    IEnumerator CicloDeVida()
    {
        // 1. APARECER (Crecer)
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / tiempoEscalado;
            transform.localScale = Vector3.Lerp(Vector3.zero, escalaOriginal, t);
            yield return null;
        }

        // 2. ADVERTENCIA (Parpadeo) - Solo si es malo
        // Hacemos que parpadee DURANTE los últimos segundos de su vida
        if (gameObject.CompareTag("topo malo"))
        {
            // Espera un poco antes de empezar a parpadear
            yield return new WaitForSeconds(Mathf.Max(0, tiempoDeVida * 0.3f));

            // Parpadea rápido 3 veces
            for (int i = 0; i < 3; i++)
            {
                if (rend != null) rend.material.color = colorAlerta;
                yield return new WaitForSeconds(0.1f);
                if (rend != null) rend.material.color = colorOriginal;
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            // Si es bueno, solo espera tranquilamente
            yield return new WaitForSeconds(tiempoDeVida);
        }

        // 3. DESAPARECER (Encogerse)
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / tiempoEscalado;
            transform.localScale = Vector3.Lerp(escalaOriginal, Vector3.zero, t);
            yield return null;
        }

        Destroy(gameObject);
    }

    public void MarcarComoGolpeado()
    {
        fueGolpeado = true;
        StopAllCoroutines();
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (gameObject.CompareTag("topo malo") && !fueGolpeado)
        {
            if (martillo != null) martillo.SumarEngañado();
        }
    }
}