using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Maximum move speed in units/second")]
    private float maxMoveSpeed;

    [SerializeField]
    private float destinationReachedTolerance = 0.5f;

    private Rigidbody2D rb;

    private Transform destination;

    public void SetDestination(Vector3 position)
    {
        if (destination == null)
        {
            GameObject go = new GameObject();
            destination = go.transform;
        }
        destination.position = position;

        Vector3 direction = (destination.position - transform.position).normalized;
        rb.velocity = direction * maxMoveSpeed;
    }

    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (destination != null)
        {
            float distance = Vector3.Distance(transform.position, destination.position);
            if (distance < destinationReachedTolerance)
            {
                rb.velocity = Vector3.zero;
                Destroy(destination.gameObject);
                destination = null;
            }
        }

    }
}
