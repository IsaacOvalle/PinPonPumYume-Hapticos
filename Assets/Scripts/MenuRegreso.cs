using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRegreso : MonoBehaviour
{
    public void RegresarMP()
    {
        Debug.Log("Regresando");
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }


    public void Salir()
    {
        Application.Quit();

    }
}
