using UnityEngine;
using UnityEngine.UI; // Necesario para cambiar el Sprite
using UnityEngine.SceneManagement;

public class GestionFinal : MonoBehaviour
{
    public GameObject panelFinal;
    public Image imagenDisplay; // Arrastra aquí el componente Image del panel
    public Sprite spriteGanaste;
    public Sprite spritePerdiste;

    public void MostrarResultado(bool gano)
    {
        panelFinal.SetActive(true); // Aparece el panel
        Time.timeScale = 0; // Congela el juego

        // Elegimos el sprite según el resultado
        if (gano)
        {
            imagenDisplay.sprite = spriteGanaste;
        }
        else
        {
            imagenDisplay.sprite = spritePerdiste;
        }

        // Liberar el mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // FUNCIÓN PARA EL BOTÓN "VOLVER A JUGAR"
    public void VolverAJugar()
    {
        Time.timeScale = 1; // RESETEAMOS EL TIEMPO PARA QUE EL JUEGO CORRA
        // Carga la escena en la que estás actualmente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void IrAlMenu()
    {
        Time.timeScale = 1; // ¡IMPORTANTE! Resetear el tiempo antes de irse
        SceneManager.LoadScene("SampleScene"); // Pon el nombre exacto de tu escena de menú
    }
}