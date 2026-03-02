using UnityEngine;
using TMPro; // IMPORTANTE: Para que reconozca el texto

public class MartilloGolpe : MonoBehaviour
{
    public float velocidadGolpe = 15f;
    public float anguloGolpe = -45f;
    public float distanciaGolpe = 20f;

    // VARIABLES PARA EL PUNTAJE
    public TextMeshProUGUI contadorTexto;
    private int puntos = 0;

    private Quaternion rotacionOriginal;
    private bool golpeando = false;

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
            if (hit.collider.CompareTag("topo"))
            {
                puntos++; // Sumamos 1 punto
                ActualizarInterfaz();
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void ActualizarInterfaz()
    {
        contadorTexto.text = "ANIQUILADOS: " + puntos;
    }

    // ... aquí abajo dejas el Coroutine AnimarGolpe igual que lo tenías ...
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