using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Libreria necesaria para el cambio de escena.
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public string gameScene;
    public void PlayGame() //Funcion que se llama en los eventos de boton.
    {
        SceneManager.LoadScene(gameScene, LoadSceneMode.Additive);
    }

   
    public void QuitGame() //Funcion que se llama en los eventos de boton.
    {
        Application.Quit(); //Termina la aplicacion
        Debug.Log("Se murio"); //Manda un mensaje
    }
}
