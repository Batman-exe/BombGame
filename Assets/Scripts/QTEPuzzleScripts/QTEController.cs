using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QTEController : MonoBehaviour
{
    //QTE = Quick Time Events
    [SerializeField] private TextMeshProUGUI displayKey;
    [SerializeField] private Slider countDownSlider;
    [SerializeField] private List<GameObject> lights;
    private int lightsIndex = 0;

    private bool QTEInProgress = false;
    private int randomQTENumber;
    private bool correctKey;
    private bool countingDown;
    //Cuantas rondas de QTE van a suceder
    private int howManyQTEs = 1;
    //Para verificar si gana o pierde
    private int howManyRigth = 1;

    private bool sliderShouldMove = true;

    //Para el counter al principio
    private bool gameStartCounter = true;
    private bool gameStartCounterInProgress = false;


    private BombQTE bomb;

    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject gameOver;

    private void Start()
    {
        gameOver = GameObject.Find("GameOverCanvas");
        FindObjectOfType<CharacterMovement>().ableToMove = false;
    }

    private void Awake()
    {
        countDownSlider.value = countDownSlider.maxValue;
        sliderShouldMove = false;
        gameStartCounter = true;
        howManyQTEs = 1;
    }

    private void Update()
    {
        if(timer.GetComponent<Timer>().timeIsUp == true)
        {
            Debug.Log("boom");
            Destroy(gameObject, 2f);
            gameOver.transform.GetChild(0).gameObject.SetActive(true);

        }

        //Funcion para ejecutar el juego principal
        if (!gameStartCounter)
        {
            QTEexecute();
        } else if (!gameStartCounterInProgress)
        {
            gameStartCounterInProgress = true;
            StartCoroutine(WaitBeforeStart());
        }

        //Actualiza el valor del slider en pantalla
        if (sliderShouldMove)
        {
            sliderValue();
        }


    }


    IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(0.5f);
        displayKey.text = "2";
        displayKey.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(1f);
        displayKey.text = "1";
        displayKey.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(1.5f);
        gameStartCounter = false;

    }


    private void QTEexecute()
    {
        //Revisa si ya hay un QTE activo y si ya se alcanzo el maximo de QTEs
        if (!QTEInProgress && howManyQTEs <= 4)
        {
            if (howManyQTEs == 4 && howManyRigth >= 3)
            {
                bomb.Defuse();
                FindObjectOfType<CharacterMovement>().ableToMove = true;
                Destroy(gameObject, 1f);
            } else if (howManyQTEs == 4 && howManyRigth <3)
            {
                Destroy(gameObject, 2f);
                gameOver.transform.GetChild(0).gameObject.SetActive(true);


            }

            howManyQTEs++;
            countDownSlider.value = countDownSlider.maxValue;
            sliderShouldMove = true;

            //Elegir un index para saber que letra presionar de manera random
            randomQTENumber = UnityEngine.Random.Range(0, 4);
            countingDown = true;
            StartCoroutine(CountDown());

            //Se muestra en pantalla la letra a presionar y una animacion para resaltar el cambio
            if (randomQTENumber == 0)
            {
                QTEInProgress = true;
                displayKey.text = "[W]";
                displayKey.GetComponent<Animation>().Play();
            }
            if (randomQTENumber == 1)
            {
                QTEInProgress = true;
                displayKey.text = "[A]";
                displayKey.GetComponent<Animation>().Play();

            }
            if (randomQTENumber == 2)
            {
                QTEInProgress = true;
                displayKey.text = "[S]";
                displayKey.GetComponent<Animation>().Play();

            }
            if (randomQTENumber == 3)
            {
                QTEInProgress = true;
                displayKey.text = "[D]";
                displayKey.GetComponent<Animation>().Play();

            }
        }

        //Se verifica si la tecla presionada coincide con la solicitada

        if (randomQTENumber == 0)
        {
            if (Input.anyKeyDown)
            {

                if (Input.GetKeyDown(KeyCode.W))
                {
                    correctKey = true;
                    StartCoroutine(KeyPressed());
                }
                else
                {
                    correctKey = false;


                    StartCoroutine(KeyPressed());
                }
            }

        }

        if (randomQTENumber == 1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    correctKey = true;
                    StartCoroutine(KeyPressed());
                }
                else
                {
                    correctKey = false;
                    StartCoroutine(KeyPressed());
                }
            }

        }

        if (randomQTENumber == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    correctKey = true;
                    StartCoroutine(KeyPressed());
                }
                else
                {
                    correctKey = false;
                    StartCoroutine(KeyPressed());
                }
            }

        }

        if (randomQTENumber == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    correctKey = true;
                    StartCoroutine(KeyPressed());
                }
                else
                {
                    correctKey = false;
                    StartCoroutine(KeyPressed());
                }
            }

        }
    }



    //Prende la luz buena o mala dependiendo de si coinciden las teclas
    //Resetea variables a valores origianales para poder dar otro ciclo
    IEnumerator KeyPressed()
    {
        randomQTENumber = 5;
        sliderShouldMove = false;

        if (correctKey)
        {
            countingDown = false;
            ligthtsOnOrOff(true);
            howManyRigth++;

            yield return new WaitForSeconds(0.5f);
            correctKey = false;
            yield return new WaitForSeconds(0.5f);
            QTEInProgress = false;

        } else
        {
            countingDown = false;
            ligthtsOnOrOff(false);

            yield return new WaitForSeconds(0.5f);
            correctKey = false;
            yield return new WaitForSeconds(0.5f);
            QTEInProgress = false;
        }



    }


    //Si pasa 1 segundo sin responder prende la luz mala y resetea los valores
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1f);

        if (countingDown)
        {
            randomQTENumber = 5;
            countingDown = false;
            ligthtsOnOrOff(false);

            yield return new WaitForSeconds(0.5f);
            correctKey = false;
            yield return new WaitForSeconds(0.5f);
            QTEInProgress = false;
        }

    }


    //Busca el GameObject luz y dependiendo de si se quiere la buena o mala se prende la correspondiente
    private void ligthtsOnOrOff(bool answer)
    {
        if (answer)
        {
            lights[lightsIndex].transform.GetChild(1).gameObject.SetActive(false);
            lights[lightsIndex].transform.GetChild(0).gameObject.SetActive(true);
        }
        if(!answer)
        {
            lights[lightsIndex].transform.GetChild(1).gameObject.SetActive(false);
            lights[lightsIndex].transform.GetChild(2).gameObject.SetActive(true);
        }
        //Para no referenciar la misma luz se aumenta el index
        lightsIndex += 1;
    }

    //Actualiza el slider
    private void sliderValue()
    {
        countDownSlider.value -= Time.deltaTime * 1;
    }

    public void SetBomb(BombQTE bombQTE)
    {
        bomb = bombQTE;
    }
}
