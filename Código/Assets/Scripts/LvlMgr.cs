using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LvlMgr : MonoBehaviour
{
    public void CargarNivel(string pNombreNivel)
    {
        SceneManager.LoadScene(pNombreNivel);
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
