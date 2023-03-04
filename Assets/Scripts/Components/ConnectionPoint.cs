using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public Rigidbody2D Rigidbody => rb;

    public GameObject ConnectedRope { get; set; }
}
