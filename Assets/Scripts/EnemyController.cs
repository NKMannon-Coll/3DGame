using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{

    private NavMeshAgent navAgent;
    private PlayerMovement player;
    private Animator animator;

    [SerializeField] private float walkingSpeed = 1;
    [SerializeField] private float attackDistance = 1;
    [SerializeField] private float attackTimeOffset = 0.1f;

    private Ray hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<PlayerMovement>();
        animator = GetComponent<Animator>();
        navAgent.speed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.SetDestination(player.gameObject.transform.position);
    }

    private void FixedUpdate()
    {

        CheckPlayerNear();
    }

    private void CheckPlayerNear() 
    {
        if (Physics.Raycast(transform.position, transform.forward * attackDistance, attackDistance)) 
        {
            Debug.Log("Yum");
            animator.SetTrigger("Attack");
            float time = 0;
            while (time < attackTimeOffset) 
            {
                time+= Time.deltaTime;
            }
            Attack();
        }
    }

    private void Attack() 
    {
        //Ray ray = new Ray(transform.)
        if (Physics.Raycast(transform.position, transform.forward * attackDistance, out RaycastHit hitInfo, attackDistance))
        {
            Debug.Log(hitInfo.rigidbody.gameObject.name);
        }
    }
}
