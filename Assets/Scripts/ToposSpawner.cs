using UnityEngine;
using System.Collections.Generic;

public class GeneradorTopos : MonoBehaviour
{
    public GameObject topoPrefab;      // Aquí arrastra tu Prefab de gato/topo
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
        Transform puntoElegido = puntosHoyos[indiceAzar];

        // Creamos el topo en ese punto
        GameObject nuevoTopo = Instantiate(topoPrefab, puntoElegido.position, puntoElegido.rotation);
        nuevoTopo.transform.Rotate(-90, 0, 0);

        // Le decimos que se destruya solo después de un rato si no lo golpean
        Destroy(nuevoTopo, tiempoVidaTopo);
    }
}