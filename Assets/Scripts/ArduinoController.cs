using UnityEngine;
using System.IO.Ports;
using System;
public class ArduinoController : MonoBehaviour
{
    public SerialPort SP;
    public MartilloGolpe Hammerhead;
    public GameObject menupausa;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SP = new SerialPort("COM3", 9600);
        SP.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if(SP.IsOpen && SP.BytesToRead > 0)
        {
            string mensaje = SP.ReadLine();
            if (mensaje.StartsWith("S"))
            {
                 StartCoroutine(Hammerhead.AnimarGolpe());
                Hammerhead.DetectarImpacto();
                Debug.Log("Se ha golpeado");
            }
            else if (mensaje == "Stop")
            {
                menupausa.SetActive(true);
                Debug.Log("Se ha pausado");
            }
            else if(mensaje == "Go")
            {
                menupausa.SetActive(false);
                Debug.Log("Se ha renaudado");
            }
        }
    }
    void OnApplicationQuit()
    {
        if (SP !=null && SP.IsOpen)
        {
            SP.Close();
        }
    }
}
