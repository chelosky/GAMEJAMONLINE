using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public AudioSource efecto;

    public void CambiarEscena(string nombre)
    {
        efecto.Play();
        SceneManager.LoadScene(nombre);
    }
    public void Salir()
    {
        efecto.Play();
        Application.Quit();
    }
}
