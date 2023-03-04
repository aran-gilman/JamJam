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

    [SerializeField]
    private float destinationReachedTolerance = 0.5f;

    private Rigidbody2D rb;
    private Vector2 mousePos;
    private Camera mainCamera;

    public Vector3 CurrentDestination { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void OnMove(InputValue value)
    {
        Vector2 inVector = value.Get<Vector2>();
        rb.velocity = inVector * maxMoveSpeed;
    }

    private void OnPoint(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    private void OnMoveTo()
    {
        CurrentDestination = mainCamera.ScreenToWorldPoint(mousePos);
        CurrentDestination = new Vector3(CurrentDestination.x, CurrentDestination.y, 0);
        Vector3 direction = (CurrentDestination - transform.position).normalized;
        rb.velocity = direction * maxMoveSpeed;
    }

    private void Update()
    {
        if (rb.velocity.magnitude > 0)
        {
            float distance = Vector3.Distance(transform.position, CurrentDestination);
            Debug.Log(distance);
            if (distance < destinationReachedTolerance)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}
