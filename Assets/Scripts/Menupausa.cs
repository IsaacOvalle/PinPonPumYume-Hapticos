using UnityEngine;

public class Menupausa : MonoBehaviour
{
    public GameObject menupausa;
    public bool Juegopause = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Juegopause)
            {
                Renaudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Renaudar()
    {
        menupausa.SetActive(false);
        Time.timeScale = 1;
        Juegopause = false;

        // ESTO REANUDA EL SONIDO
        AudioListener.pause = false;
    }

    public void Pausar()
    {
        menupausa.SetActive(true);
        Time.timeScale = 0;
        Juegopause = true;

        // ESTO PAUSA EL SONIDO
        AudioListener.pause = true;
    }
}