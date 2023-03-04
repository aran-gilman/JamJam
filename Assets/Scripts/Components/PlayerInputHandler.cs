using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private float clickCooldownDuration = 0.1f;

    [SerializeField]
    private GameObject ropePrefab;

    private Vector2 mousePos;
    private Camera mainCamera;

    private GameObject player;
    private float clickCooldown = 0.0f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (clickCooldown > 0)
        {
            clickCooldown -= Time.deltaTime;
        }
    }

    private void OnPoint(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    private void OnClick()
    {
        if (clickCooldown > 0)
        {
            return;
        }
        clickCooldown = clickCooldownDuration;

        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        Collider2D[] cols = Physics2D.OverlapPointAll(worldPos);

        bool foundObject = false;
        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("Player"))
            {
                if (player == col.gameObject)
                {
                    player = null;
                    Debug.Log("Deselected player");
                }
                else
                {
                    player = col.gameObject;
                    Debug.Log("Selected player");
                }
                foundObject = true;
            }
        }

        if (!foundObject && player != null)
        {
            player.GetComponent<PlayerMovement>().SetDestination(worldPos);
        }
    }

    private void OnConnect()
    {
        if (clickCooldown > 0 || player == null)
        {
            return;
        }
        clickCooldown = clickCooldownDuration;

        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        Collider2D[] cols = Physics2D.OverlapPointAll(worldPos);

        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("ConnectionPoint"))
            {
                Debug.Log("Clicked connection point");
                //ConnectionPoint point = col.GetComponent<ConnectionPoint>();
            }
        }
    }
}
