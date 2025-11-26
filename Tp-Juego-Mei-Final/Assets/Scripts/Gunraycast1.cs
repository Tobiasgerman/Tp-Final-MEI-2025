using UnityEngine;

public class Gunraycast1 : MonoBehaviour
{
    [Header("Ray settings")]
    public float range = 50f;
    public float force = 500f;
    public LayerMask hitLayers = ~0; // por defecto todo
    public bool useCameraCenter = true; // si true usa cámara principal y centro de pantalla

    [Header("Door")]
    public GameObject door;
    public string targetTag = "Objetivo";

    [Header("Visual / Debug")]
    public bool drawDebugRay = true;
    public float debugDuration = 1f;
    public LineRenderer lineRenderer; // opcional: arrastrar LineRenderer para ver láser

    Camera cam;

    void Start()
    {
        if (useCameraCenter)
        {
            cam = Camera.main;
            if (cam == null)
                Debug.LogWarning("No hay Main Camera. Desactiva useCameraCenter o asigna la cámara principal con tag MainCamera.");
        }
    }

    void Update()
    {
        // usa Input.GetMouseButtonDown(0) si prefieres
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 origin;
        Vector3 direction;

        if (useCameraCenter && cam != null)
        {
            // Ray desde el centro de la pantalla (FPS típico)
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            origin = ray.origin;
            direction = ray.direction;
        }
        else
        {
            // Ray desde la posición de este transform hacia forward
            origin = transform.position;
            direction = transform.forward;
        }

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, range, hitLayers))
        {
            Debug.Log($"Golpeaste: {hit.collider.name} (Tag: {hit.collider.tag})");

            // Fuerza
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(direction * force, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("El objeto no tiene Rigidbody (no se aplicó fuerza).");
            }

            // Si el objeto golpeado se llama "cartel", lo desactivamos
            if (hit.collider.gameObject.name == "cartel")
            {
                hit.collider.gameObject.SetActive(false);
                Debug.Log("Cartel desactivado.");
            }

        }
    
        else
        {
            Debug.Log("No golpeaste nada.");
        }

        // dibujar rayo para debug
        if (drawDebugRay)
        {
            Debug.DrawRay(origin, direction * range, Color.red, debugDuration);
        }

        // LineRenderer opcional (visual láser)
        if (lineRenderer != null)
        {
            StartCoroutine(ShowLaser(origin, direction));
        }
    }

    System.Collections.IEnumerator ShowLaser(Vector3 origin, Vector3 direction)
    {
        float t = 0f;
        float duration = 0.08f;

        Vector3 endPoint = origin + direction * range;
        if (Physics.Raycast(origin, direction, out RaycastHit h, range, hitLayers))
        {
            endPoint = h.point;
        }

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, endPoint);

        while (t < duration)
        {
            t += Time.deltaTime;
            yield return null;
        }

        lineRenderer.positionCount = 0;
    }
}
