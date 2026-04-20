using UnityEngine;
using TMPro;
using System.IO.Ports;

public class MartilloGolpe : MonoBehaviour
{
    
    [Header("Configuración de Golpe")]
    public float velocidadGolpe = 15f;
    public float anguloGolpe = -45f;
    public float distanciaGolpe = 20f;

    [Header("Efectos y Prefabs")]
    public GameObject prefabPuntosMas20;
    public GameObject prefabPuntosMenos5;

    [Header("Interfaz UI")]
    public TextMeshProUGUI contadorAniquilados;
    public TextMeshProUGUI contadorPuntos;
    public TextMeshProUGUI contadorEngañados;
    public TextMeshProUGUI contadorCombo;
    public TextMeshProUGUI contadorTiempo; // NUEVO: Arrastra aquí el texto del cronómetro

    [Header("Reglas de Juego")]
    public float tiempoRestante = 60f; // El tiempo que elijas (ej. 60 segundos)
    public int puntosParaGanar = 500; // La meta de puntos
    private bool juegoTerminado = false;

    private int aniquilados = 0;
    private int puntosTotales = 0;
    private int engañados = 0;
    private int malosGolpeadosSeguidos = 0;

    private Quaternion rotacionOriginal;
    private bool golpeando = false;

    [Header("Referencias Externas")]
    public HammerSoundControler HSC;
    public Menupausa scriptMenuPausa;
    public GestionFinal scriptFinal;
    public float rotX = 0f, rotY = 180f, rotZ = 0f;

    void Start()
    {
        rotacionOriginal = transform.localRotation;
        ActualizarInterfaz();
    }

    void Update()
    {
        if (juegoTerminado) return;

        // --- LÓGICA DEL CRONÓMETRO ---
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarInterfaz(); // Actualizamos para que se vea el segundero
        }
        else
        {
            tiempoRestante = 0;
            FinalizarJuego(false); // Si llega a 0 y no ganó, pierde
        }

        // --- ENTRADA DE GOLPE ---
        if (Input.GetMouseButtonDown(0) && !golpeando)
        {
            StartCoroutine(AnimarGolpe());
            DetectarImpacto();
        }
    }

    public void DetectarImpacto()
    {
        RaycastHit hit;
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayo, out hit, distanciaGolpe))
        {
            Topo scriptTopo = hit.collider.GetComponent<Topo>();

            if (hit.collider.CompareTag("topo bueno"))
            {
                if (scriptTopo != null) scriptTopo.MarcarComoGolpeado();
                aniquilados++;
                puntosTotales += 50;
                ProcesarAcierto(hit.collider.transform.position, prefabPuntosMas20);
            }

            if (hit.collider.CompareTag("topo malo"))
            {
                if (scriptTopo != null) scriptTopo.MarcarComoGolpeado();
                aniquilados++;
                puntosTotales += 10;
                malosGolpeadosSeguidos++;

                if (malosGolpeadosSeguidos >= 2)
                {
                    if (engañados > 0) engañados--;
                    malosGolpeadosSeguidos = 0;
                }
                ProcesarAcierto(hit.collider.transform.position, prefabPuntosMenos5);
            }
        }
    }

    void ProcesarAcierto(Vector3 pos, GameObject efecto)
    {
        AparecerEfectoPuntos(efecto, pos);
        ActualizarInterfaz();
        HSC.Golpear();
        // Nota: El objeto se destruye dentro de scriptTopo.MarcarComoGolpeado() 
        // o puedes dejar el Destroy aquí si prefieres.

        if (puntosTotales >= puntosParaGanar)
        {
            FinalizarJuego(true);
        }
    }

    void FinalizarJuego(bool esVictoria)
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        if (scriptFinal != null) { scriptFinal.MostrarResultado(esVictoria); }
        else if (scriptMenuPausa != null) { scriptMenuPausa.Pausar(); }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SumarEngañado()
    {
        if (juegoTerminado) return;

        engañados++;
        malosGolpeadosSeguidos = 0;
        puntosTotales = Mathf.Max(0, puntosTotales - 30);
        ActualizarInterfaz();

        if (engañados >= 5) { FinalizarJuego(false); }
    }

    public void AparecerEfectoPuntos(GameObject prefab, Vector3 posicion)
    {
        if (prefab != null)
        {
            GameObject efecto = Instantiate(prefab, posicion + Vector3.up * 0.5f, Quaternion.identity);
            efecto.transform.LookAt(Camera.main.transform);
            efecto.transform.Rotate(rotX, rotY, rotZ);
            Destroy(efecto, 0.5f);
        }
    }

    public void ActualizarInterfaz()
    {
        contadorAniquilados.text = "ANIQUILADOS: " + aniquilados;
        contadorPuntos.text = "PUNTOS: " + puntosTotales + "/" + puntosParaGanar;
        contadorEngañados.text = "LOCURA: " + engañados;

        if (contadorCombo != null)
            contadorCombo.text = "CURA: " + malosGolpeadosSeguidos + "/2";

        if (contadorTiempo != null)
            contadorTiempo.text = " " + Mathf.Ceil(tiempoRestante).ToString() + "s";
    }

    public System.Collections.IEnumerator AnimarGolpe()
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