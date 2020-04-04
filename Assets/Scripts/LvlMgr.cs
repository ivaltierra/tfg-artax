using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LvlMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CargarNivel(string pNombreNivel){
        SceneManager.LoadScene(pNombreNivel);
    }

    public void SalirJuego(){
        Application.Quit();
    }
}
