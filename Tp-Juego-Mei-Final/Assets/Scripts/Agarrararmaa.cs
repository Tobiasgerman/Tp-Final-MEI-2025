using UnityEngine;

public class Agarrararmaa : MonoBehaviour
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

       
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

       
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

      
        GetComponent<Gunraycast1>().enabled = true;

        Debug.Log("Arma recogida.");
    }
}
