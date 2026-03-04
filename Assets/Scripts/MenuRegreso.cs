using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRegreso : MonoBehaviour
{
    public void RegresarMP()
    {
        Debug.Log("Regresando");
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }


    public void Salir()
    {
        Application.Quit();

    }
}
