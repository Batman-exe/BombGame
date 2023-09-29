using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public LineRenderer line;
    [SerializeField] private string destinationTag;
    [SerializeField] private GameObject lightOn;

    Vector3 offset;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }


    private void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
    }

    // Mueve el componente "cable" desde el inicio del cable hasta donde arrastre el mouse
    private void OnMouseDrag()
    {

        line.SetPosition(0, MouseWorldPosition() + offset);
        line.SetPosition(1, transform.position);

    }

    //Hace un raycast en la posicion donde suelte el mouse para verificar si esta sobre el cable correspondiente
    private void OnMouseUp()
    {
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDir = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(rayOrigin, rayDir);

        if (Physics2D.Raycast(rayOrigin, rayDir))
        {
            //Compara el tag del cable inicial con el que hace contacto
            //Si es igual enciende la luz y desactiva el collider para que no pueda activarse de nuevo
            //Si no es igual se regresa a su posicion inicial
            if (hitInfo.transform.tag == destinationTag)
            {
                line.SetPosition(0, hitInfo.transform.position);
                lightOn.SetActive(true);
                transform.gameObject.GetComponent<Collider2D>().enabled = false;
            } else
            {
                line.SetPosition(0, transform.position);
            }
        }

    }


    private Vector3 MouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }


}
