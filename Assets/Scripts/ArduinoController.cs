using UnityEngine;
using System.IO.Ports;
using System;
public class ArduinoController : MonoBehaviour
{
    public SerialPort SP;
    public MartilloGolpe Hammerhead;
    public Menupausa menupausa;
    public bool sensoresActivos = true;
    public Transform punto1;
    public Transform punto2;
    public Transform punto3;
    public Transform punto4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SP = new SerialPort("COM3", 9600);
        SP.ReadTimeout = 50;
        SP.Open();
    }

    // Update is called once per frame
    void Update()
    {
        try {
            if (SP.IsOpen && SP.BytesToRead > 0)
            {
                string mensaje = SP.ReadLine().Trim();
                 if (mensaje == "Stop")
                {
                    Debug.Log("¡Recibí la orden de Stop desde Arduino!");
                    menupausa.Pausar();
                    sensoresActivos = false;
                    if (menupausa.menupausa == null) Debug.LogError("¡No has asignado el panel en el Inspector!");
                    Debug.Log("Se ha pausado");
                }
                else if (mensaje == "Go")
                {
                    menupausa.Renaudar();
                    sensoresActivos = true;
                    Debug.Log("Se ha renaudado");
                }
                if (mensaje.StartsWith("S"))
                {
                    StartCoroutine(Hammerhead.AnimarGolpe());
                    //Hammerhead.DetectarImpacto();
                    if (mensaje.Contains("Sensor 1"))
                    {
                        Hammerhead.DetectarImpacto(punto3);
                        Debug.Log("Se ha golpeado 1");
                        Debug.Log("Mensaje recibido: [" + mensaje + "]");
                    }
                    else if (mensaje.Contains("Sensor 2"))
                    {
                        Hammerhead.DetectarImpacto(punto1);
                        Debug.Log("Se ha golpeado 2");
                        Debug.Log("Mensaje recibido: [" + mensaje + "]");
                    }
                    else if (mensaje.Contains("Sensor 3"))
                    {
                        Hammerhead.DetectarImpacto(punto4);
                        Debug.Log("Se ha golpeado 3");
                        Debug.Log("Mensaje recibido: [" + mensaje + "]");
                    }
                    else if (mensaje.Contains("Sensor 4"))
                    {
                        Hammerhead.DetectarImpacto(punto2);
                        Debug.Log("Se ha golpeado 4");
                        Debug.Log("Mensaje recibido: [" + mensaje + "]");
                    }
                    Debug.Log("Se ha golpeado");
                }
              
            }
        }
        catch (System.TimeoutException) { }
        catch (System.Exception ex) { Debug.LogWarning("Error leyendo el puerto serial: " + ex.Message); }
        
    }
    void OnApplicationQuit()
    {
        if (SP !=null && SP.IsOpen)
        {
            SP.Close();
        }
    }
}
