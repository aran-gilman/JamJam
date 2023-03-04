using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Maximum move speed in units/second")]
    private float maxMoveSpeed;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        Vector2 inVector = value.Get<Vector2>();
        rb.velocity = inVector * maxMoveSpeed;
    }
}
