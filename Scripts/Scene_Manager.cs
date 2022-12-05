using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene("Juego");
    }
    
    public void LoadControles()
    {
        SceneManager.LoadScene("Controles");
    }

    public void LoadInstrucciones()
    {
        SceneManager.LoadScene("Instrucciones");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Pantalla Inicial");
    }
}
