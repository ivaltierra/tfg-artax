using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class esperar : MonoBehaviour
{
    public float t_espera = 8f;
    public int nScene = 1;
    void Start()
    {
        StartCoroutine(esperarIntro());        
    }

    IEnumerator esperarIntro()
    {
       yield return new WaitForSeconds(t_espera);
       SceneManager.LoadScene(nScene); 
    }
}
