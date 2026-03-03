using UnityEngine;

public class HammerSoundControler : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip SonidoGolpe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Golpear()
    {
        audiosource.PlayOneShot(SonidoGolpe);
    }
}
