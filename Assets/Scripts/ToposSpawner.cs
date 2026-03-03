using UnityEngine;
using System.Collections.Generic;

public class GeneradorTopos : MonoBehaviour
{
    public GameObject[] toposPrefab;      // Aquí arrastra tu Prefab de gato/topo
    public Transform[] puntosHoyos;    // Aquí arrastra tus 6 puntos vacíos
    public float tiempoAparicion = 2f; // Cada cuántos segundos sale uno
    public float tiempoVidaTopo = 1.5f; // Cuánto tiempo se queda antes de esconderse

    void Start()
    {
        // Empezamos a llamar a la función repetidamente
        InvokeRepeating("AparecerTopo", 1f, tiempoAparicion);
    }

    void AparecerTopo()
    {
        // Elegimos un hoyo al azar de la lista
        int indiceAzar = Random.Range(0, puntosHoyos.Length);
        int indiceAzar2 = Random.Range(0, toposPrefab.Length);
        Transform puntoElegido = puntosHoyos[indiceAzar];
        GameObject topoElegido = toposPrefab[indiceAzar2];

        // Creamos el topo en ese punto
        GameObject nuevoTopo = Instantiate(topoElegido, puntoElegido.position, puntoElegido.rotation);
        nuevoTopo.transform.Rotate(-90, 180, 0);

        // Le decimos que se destruya solo después de un rato si no lo golpean
        Destroy(nuevoTopo, tiempoVidaTopo);
    }
}