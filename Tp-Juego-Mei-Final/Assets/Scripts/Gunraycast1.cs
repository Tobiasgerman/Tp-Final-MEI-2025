using UnityEngine;

public class Gunraycast1 : MonoBehaviour
{
    [Header("Ray settings")]
    public float range = 50f;
    public float force = 500f;
    public LayerMask hitLayers = ~0; 
    public bool useCameraCenter = true; 

    [Header("Door")]
    public GameObject door;
    public string targetTag = "Objetivo";

    [Header("Visual / Debug")]
    public bool drawDebugRay = true;
    public float debugDuration = 1f;
    public LineRenderer lineRenderer; 

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
            
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            origin = ray.origin;
            direction = ray.direction;
        }
        else
        {
            
            origin = transform.position;
            direction = transform.forward;
        }

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, range, hitLayers))
        {
            Debug.Log($"Golpeaste: {hit.collider.name} (Tag: {hit.collider.tag})");

            
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(direction * force, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("El objeto no tiene Rigidbody (no se aplicó fuerza).");
            }

           
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

       
        if (drawDebugRay)
        {
            Debug.DrawRay(origin, direction * range, Color.red, debugDuration);
        }

        
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
