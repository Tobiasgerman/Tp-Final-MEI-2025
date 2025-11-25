using UnityEngine;

public class Gunraycast1 : MonoBehaviour
{
    public float range = 50f;
    public float force = 500f;
    public GameObject door;
    public string targetTag = "Objetivo";

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Click izquierdo
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log("Golpeaste: " + hit.collider.name);

            // Empuje AddForce
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
            }

            // Abrir puerta
            if (hit.collider.CompareTag(targetTag))
            {
                if (door != null)
                    door.SetActive(false);
            }
        }
    }
}

