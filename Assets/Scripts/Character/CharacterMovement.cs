using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [Tooltip("Speed of the character movement.")]
    [SerializeField] private float moveSpeed = 250.0f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator characterAnimator;
    public bool ableToMove;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        ableToMove = true;
    }

    private void Update()
    {
        if(ableToMove)
        {
            // Gets inputs for horizontal and vertical movement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(horizontalInput, verticalInput).normalized;

            characterAnimator.SetFloat("Horizontal", horizontalInput);
            characterAnimator.SetFloat("Vertical", verticalInput);
            characterAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed * Time.fixedDeltaTime;
    }
}
