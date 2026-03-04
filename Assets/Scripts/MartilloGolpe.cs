using UnityEngine;
using TMPro;

public class MartilloGolpe : MonoBehaviour
{
    public float velocidadGolpe = 15f;
    public float anguloGolpe = -45f;
    public float distanciaGolpe = 20f;

    public GameObject prefabPuntosMas20;
    public GameObject prefabPuntosMenos5;

    public TextMeshProUGUI contadorAniquilados;
    public TextMeshProUGUI contadorPuntos;
    public TextMeshProUGUI contadorEngañados;

    private int aniquilados = 0;
    private int puntosTotales = 0;
    private int engañados = 0;

    private Quaternion rotacionOriginal;
    private bool golpeando = false;

    public HammerSoundControler HSC;
    public Menupausa scriptMenuPausa;
    public GestionFinal scriptFinal; // Referencia para las pantallas de victoria/derrota
    public float rotX = 0f, rotY = 180f, rotZ = 0f;

    void Start()
    {
        rotacionOriginal = transform.localRotation;
        ActualizarInterfaz();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !golpeando)
        {
            StartCoroutine(AnimarGolpe());
            DetectarImpacto();
        }
    }

    void DetectarImpacto()
    {
        RaycastHit hit;
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayo, out hit, distanciaGolpe))
        {
            // --- TOPO BUENO ---
            if (hit.collider.CompareTag("topo bueno"))
            {
                aniquilados++;
                puntosTotales += 50;

                AparecerEfectoPuntos(prefabPuntosMas20, hit.collider.transform.position);
                ActualizarInterfaz();
                HSC.Golpear();
                Destroy(hit.collider.gameObject);

                // CONDICIÓN DE VICTORIA (500 PUNTOS)
                if (puntosTotales >= 500)
                {
                    FinalizarJuego(true); // Enviamos 'true' porque ganó
                }
            }

            // --- TOPO MALO ---
            if (hit.collider.CompareTag("topo malo"))
            {
                engañados++;
                puntosTotales -= 30;

                AparecerEfectoPuntos(prefabPuntosMenos5, hit.collider.transform.position);
                ActualizarInterfaz();
                HSC.Golpear();
                Destroy(hit.collider.gameObject);

                // CONDICIÓN DE DERROTA (5 ENGAÑOS)
                if (engañados >= 5)
                {
                    FinalizarJuego(false); // Enviamos 'false' porque perdió
                }
            }
        }
    }

    // NUEVA FUNCIÓN MEJORADA
    void FinalizarJuego(bool esVictoria)
    {
        if (scriptFinal != null)
        {
            // Llama a la pantalla de victoria o derrota según el caso
            scriptFinal.MostrarResultado(esVictoria);
        }
        else if (scriptMenuPausa != null)
        {
            // Respaldo por si no has configurado el script final todavía
            scriptMenuPausa.Pausar();
        }

        // Liberar el mouse para poder picar los botones de las pantallas
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Mantenemos esta para compatibilidad o por si quieres pausar manualmente
    void GanarOPerder()
    {
        FinalizarJuego(false);
    }

    void AparecerEfectoPuntos(GameObject prefab, Vector3 posicion)
    {
        if (prefab != null)
        {
            GameObject efecto = Instantiate(prefab, posicion + Vector3.up * 0.5f, Quaternion.identity);
            efecto.transform.LookAt(Camera.main.transform);
            efecto.transform.Rotate(rotX, rotY, rotZ);
            Destroy(efecto, 0.5f);
        }
    }

    void ActualizarInterfaz()
    {
        contadorAniquilados.text = "ANIQUILADOS: " + aniquilados;
        contadorPuntos.text = "PUNTOS: " + puntosTotales;
        contadorEngañados.text = "LOCURA: " + engañados;
    }

    System.Collections.IEnumerator AnimarGolpe()
    {
        golpeando = true;
        Quaternion rotacionMeta = rotacionOriginal * Quaternion.Euler(anguloGolpe, 0, 0);
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * velocidadGolpe;
            transform.localRotation = Quaternion.Lerp(rotacionOriginal, rotacionMeta, t);
            yield return null;
        }
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * (velocidadGolpe / 2);
            transform.localRotation = Quaternion.Lerp(rotacionMeta, rotacionOriginal, t);
            yield return null;
        }
        golpeando = false;
    }
}