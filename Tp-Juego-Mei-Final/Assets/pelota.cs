using UnityEngine;

public class pelota : MonoBehaviour
{
    [Header("Pickup Settings")]
    public Transform hand;          // Mano del jugador
    public float pickupDistance = 3f;
    public KeyCode pickupKey = KeyCode.H;   // ← AHORA H

    [Header("Shoot Settings")]
    public float shootForce = 900f;

    [Header("Environment")]
    public GameObject environmentParent;

    private Rigidbody rb;
    private bool isPicked;
    private bool wasShot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float dist = Vector3.Distance(hand.position, transform.position);

        // AGARRAR
        if (!isPicked && dist <= pickupDistance && Input.GetKeyDown(pickupKey))
        {
            PickUp();
        }

        // SI ESTÁ AGARRADA:
        if (isPicked)
        {
            // Seguir la mano
            transform.position = hand.position;

            // DISPARAR (CLICK DERECHO)
            if (Input.GetMouseButtonDown(1))   // ← CLICK DERECHO
            {
                Shoot();
            }
        }
    }

    void PickUp()
    {
        isPicked = true;
        wasShot = false;
        rb.isKinematic = true;

        GetComponent<Collider>().enabled = false;

        transform.SetParent(hand);
    }

    void Shoot()
    {
        isPicked = false;
        wasShot = true;
        transform.SetParent(null);
        rb.isKinematic = false;

        GetComponent<Collider>().enabled = true;

        rb.AddForce(hand.forward * shootForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (wasShot)
        {
            Destroy(environmentParent);
            Destroy(gameObject);
        }
    }
}
