using System.Collections.Generic;
using UnityEngine;

public class pelota : MonoBehaviour
{
    public Transform hand;
    public float pickupDistance = 3f;
    public KeyCode pickupKey = KeyCode.H;

   
    public float shootForce = 900f;

    
    public List<GameObject> objectsToDestroy;   

    private Rigidbody rb;
    private Collider col;
    private LineRenderer lr;

    private bool isPicked = false;
    private bool wasShot = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        lr = GetComponent<LineRenderer>();

       
        lr.positionCount = 0;
        lr.widthMultiplier = 0.05f;
    }

    void Update()
    {
        float dist = Vector3.Distance(hand.position, transform.position);

        
        if (!isPicked && dist <= pickupDistance && Input.GetKeyDown(pickupKey))
        {
            PickUp();
        }

        if (isPicked)
        {
            transform.position = hand.position;

           
            if (Input.GetMouseButtonDown(1))
            {
                Shoot();
            }
        }

       
        if (wasShot)
        {
            DrawTrajectory();
        }
    }

    void PickUp()
    {
        isPicked = true;
        wasShot = false;

        rb.isKinematic = true;
        col.enabled = false;
        lr.positionCount = 0;

        transform.SetParent(hand);
    }

    void Shoot()
    {
        isPicked = false;
        wasShot = true;

        transform.SetParent(null);
        rb.isKinematic = false;
        col.enabled = true;

        rb.AddForce(hand.forward * shootForce, ForceMode.Impulse);
    }

    void DrawTrajectory()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + rb.velocity * 0.1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!wasShot) return;

       
        foreach (GameObject obj in objectsToDestroy)
        {
            if (collision.gameObject == obj)
            {
                Destroy(obj);
            }
        }

       
        lr.positionCount = 0;
        wasShot = false;
    }
}
