using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRegreso : MonoBehaviour
{
    public void RegresarMP()
    {
        Debug.Log("Regresando");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


    public void Salir()
    {
        Application.Quit();

    }
}
