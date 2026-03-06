using UnityEngine;
using System.Collections; // Necesario para Corrutinas
using System.Collections.Generic;

public class GeneradorTopos : MonoBehaviour
{
    public GameObject[] toposPrefab;
    public Transform[] puntosHoyos;
    public float tiempoAparicion = 2f;

    void Start()
    {
        StartCoroutine(RutinaSpawn());
    }

    IEnumerator RutinaSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoAparicion);

            // Lista de hoyos disponibles para no repetir en el mismo turno
            List<int> hoyosDisponibles = new List<int>();
            for (int i = 0; i < puntosHoyos.Length; i++) hoyosDisponibles.Add(i);

            // --- PRIMER TOPO ---
            SpawnAleatorio(hoyosDisponibles);

            // --- SEGUNDO TOPO (50% de probabilidad de salir) ---
            if (Random.value > 0.5f)
            {
                SpawnAleatorio(hoyosDisponibles);
            }
        }
    }

    void SpawnAleatorio(List<int> hoyosDisponibles)
    {
        if (hoyosDisponibles.Count == 0) return;

        // Elegimos un hoyo de los que sobran
        int indiceLista = Random.Range(0, hoyosDisponibles.Count);
        int indiceHoyo = hoyosDisponibles[indiceLista];
        hoyosDisponibles.RemoveAt(indiceLista); // Quitamos ese hoyo para que el 2do topo no lo use

        int indiceTopo = Random.Range(0, toposPrefab.Length);

        GameObject nuevoTopo = Instantiate(toposPrefab[indiceTopo], puntosHoyos[indiceHoyo].position, puntosHoyos[indiceHoyo].rotation);
        nuevoTopo.transform.Rotate(-90, 180, 0);

        // El tiempo de vida ya lo maneja el script Topo.cs en su Start()
    }
}