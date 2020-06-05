using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasijaRota : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 200));
        gameObject.transform.GetChild(1).GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 200));
        gameObject.transform.GetChild(2).GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 200));
        gameObject.transform.GetChild(3).GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 200));

        StartCoroutine(eliminar());
    }

    IEnumerator eliminar() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
