using UnityEngine;

public class PlayerPickUpGun : MonoBehaviour
{
    public GameObject gunOnHand;   // El arma que aparece en tu mano
    private GameObject gunOnGround; // El arma que está en el piso
    private bool canPickUp = false;

    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUpGun();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            gunOnGround = other.gameObject;
            canPickUp = true;
            Debug.Log("Presiona E para recoger el arma.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            canPickUp = false;
            gunOnGround = null;
        }
    }

    void PickUpGun()
    {
        gunOnHand.SetActive(true);     // Mostrar arma en la mano
        Destroy(gunOnGround);          // Eliminar arma del piso
        Debug.Log("Arma recogida");
    }
}
