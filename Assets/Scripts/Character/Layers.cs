using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour
{
    private Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colision detectada");
        if (collision != null && collision.gameObject.CompareTag("Behind")) {

            _renderer.sortingOrder = 1;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Ya no hay colisión");

        if (collision != null && collision.gameObject.CompareTag("Behind"))
        {

            _renderer.sortingOrder = 3;

        }
    }

}
