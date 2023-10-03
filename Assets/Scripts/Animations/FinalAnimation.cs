using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAnimation : MonoBehaviour
{
    [SerializeField] private AudioSource meteoriteSound;
    [SerializeField] private AudioSource crashSound;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private GameObject room;
    private Bomb[] bombs;


    private void Awake()
    {
        text.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke("Initialize", 3f);
    }

    /// <summary>
    /// Reproduces the sound of Crash
    /// </summary>
    private void PlayCrash()
    {
        if (crashSound != null)
            crashSound.Play();
    }

    /// <summary>
    /// Enables every object for the animation, storages the bombs that were in scene and 
    /// starts the coroutine that synchronizes everything in the Animation
    /// </summary>
    private void Initialize()
    {
        foreach(Transform child in transform)
            child.gameObject.SetActive(true);

        bombs = GameObject.FindObjectsOfType<Bomb>();

        StartCoroutine(Play());
    }

    /// <summary>
    /// Turns every bomb in scene enabled or disabled.
    /// </summary>
    /// <param name="active">true to enable bombs, false otherwise</param>
    private void SetActiveBombsInScene(bool active)
    {
        foreach (Bomb bomb in bombs)
            bomb.gameObject.SetActive(active);
    }

    IEnumerator Play()
    {
        SetActiveBombsInScene(false);
        cameraAnimator.SetBool("PlayingEnd", true);
        meteoriteSound.Play();
        room.SetActive(false);
        // Time until the meteorite crashes
        yield return new WaitForSeconds(3f);
        PlayCrash();
        // Time until all animations stop
        yield return new WaitForSeconds(3f);
        text.SetActive(true);
        // Time for reading
        yield return new WaitForSeconds(8f);
        room.SetActive(true);
        Timer timer = FindAnyObjectByType<Timer>();
        if(timer != null)
            timer.enabled = false;
        cameraAnimator.SetBool("PlayingEnd", false);
        text.SetActive(false);
        gameOver.SetActive(true);
        SetActiveBombsInScene(true);
        gameObject.SetActive(false);
    }
}
