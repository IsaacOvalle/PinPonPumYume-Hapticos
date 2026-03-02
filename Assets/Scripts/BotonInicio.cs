using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonInicio : MonoBehaviour
{
    public void Jugar()
    {
        Debug.Log("Iniciando");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    
    public void Salir()
    {
        Application.Quit();

    }
}
