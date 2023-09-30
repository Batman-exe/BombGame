using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{

    public int buttonIndex;

    [SerializeField] private ColorSequence colorSeq;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color highlightColor;
    private float resetDelay = 0.25f;

    private void Start()
    {
        ResetButton();
    }


    //Cuando se de click en el boton se resalte y se de la inforamcion del index al ColorSequence script
    public void OnMouseDown()
    {
        colorSeq.PlayersPick(buttonIndex);
        PressButton();
    }

    //funcion para darle efecto de seleccionado al boton
    public void PressButton()
    {
        GetComponent<SpriteRenderer>().color = highlightColor;
        Invoke("ResetButton", resetDelay);
    }

    //funcion para devolverlo a su color original
    public void ResetButton()
    {
        GetComponent<SpriteRenderer>().color = defaultColor;

    }




}
