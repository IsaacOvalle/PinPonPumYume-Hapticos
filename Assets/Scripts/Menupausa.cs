using UnityEngine;

public class Menupausa : MonoBehaviour
{
    public GameObject menupausa;
    public bool Juegopause = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Juegopause)
            {
                Renaudar();
            }
            else {
                Pausar();
            }
        }
    }

    public void Renaudar()
    {
        menupausa.SetActive(false);
        Time.timeScale = 1;
        Juegopause = false;
    }

    public void Pausar()
    {
        menupausa.SetActive(true);
        Time.timeScale = 0;
        Juegopause = true;
    }

}
