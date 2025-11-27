using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private Transform human;         // El jugador SimpleFPSController
    private NavMeshAgent agent;
    private Animator anim;

    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Buscar automáticamente al humano llamado SimpleFPSController
        GameObject h = GameObject.Find("SimpleFPSController");
        if (h != null)
            human = h.transform;
        else
            Debug.LogError("No se encontró un objeto llamado 'SimpleFPSController' en la escena");
    }

    void Update()
    {
        if (isDead || human == null) return;

        agent.SetDestination(human.position);
    }

    // Llamado por el rayo láser
    public void HitByLaser()
    {
        if (isDead) return;

        isDead = true;
        agent.isStopped = true;

        // Caída al piso rápida
        transform.rotation = Quaternion.Euler(90f, transform.eulerAngles.y, transform.eulerAngles.z);

        if (anim != null)
            anim.SetTrigger("Die");
    }
}
