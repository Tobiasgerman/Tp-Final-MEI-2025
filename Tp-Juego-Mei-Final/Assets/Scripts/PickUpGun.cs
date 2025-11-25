using UnityEngine;

public class PickupGun : MonoBehaviour
{
    public Transform hand;        // Mano del jugador
    public float pickupDistance = 3f;
    public KeyCode pickupKey = KeyCode.E;

    private bool isPicked = false;

    void Update()
    {
        if (isPicked) return;

        // Distancia al jugador
        float dist = Vector3.Distance(hand.position, transform.position);

        // Si está cerca y presiona E
        if (dist < pickupDistance && Input.GetKeyDown(pickupKey))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        isPicked = true;

        // Desactivar física para que no se caiga
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Poner el arma en la mano
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Habilitar el script de disparo
        GetComponent<Gunraycast1>().enabled = true;

        Debug.Log("Arma recogida.");
    }
}
