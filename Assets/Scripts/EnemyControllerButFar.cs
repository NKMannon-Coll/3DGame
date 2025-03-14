using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerButFar : MonoBehaviour
{ 

    private NavMeshAgent navAgent;
    private PlayerMovement player;
    private Animator animator;
    private EnemyThrow enemyThrow;

    private Vector3 positionCorrected;

    [SerializeField] private float walkingSpeed = 1;
    [SerializeField] private float attackDistance = 60;
    [SerializeField] private float attackTimeOffset = 0.3f;
    [SerializeField] private float attackDamage = 10;
    [SerializeField] private float health = 10;
    [SerializeField] private float stunnedDuration = 1.3f;
    [SerializeField] private int stalkingDistance = 6;

    [SerializeField] private float distance = 0;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip die;

    private float hitDeadTime = 4f;
    private float timer = 0;
    private float time = 0;
    private float stunnedTimer = 0;
    private bool dead = false;

    private bool attacked = false;
    private bool stunned = false;

    private Ray hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<PlayerMovement>();
        animator = GetComponent<Animator>();
        enemyThrow = GetComponent<EnemyThrow>();
        navAgent.speed = walkingSpeed;
        GameManager.Instance.enemyCount += 1;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
        checkStunned();
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            navAgent.SetDestination(player.gameObject.transform.position);
            CheckPlayerNear();
            KeepDistance();
        }
        //Debug.Log("what the heck");
    }

    private void CheckPlayerNear()
    {
        positionCorrected = transform.position + new Vector3(0, 0.4f, 0);
        Debug.DrawLine(positionCorrected, positionCorrected + transform.forward * attackDistance, Color.red);
        if (!attacked)
        {
            //Debug.Log("Why2");
            if (timer > hitDeadTime)
            {
                //if (Physics.Raycast(transform.position, transform.forward * attackDistance, attackDistance))
                //{
                if (Physics.Raycast(positionCorrected, player.transform.position - positionCorrected, out RaycastHit hitInfo, attackDistance))
                {
                    //Debug.Log("Why");
                    if (hitInfo.transform.gameObject.TryGetComponent(out PlayerMovement pm))
                    {
                        time = 0;
                        attacked = true;
                        //Debug.Log("Yum");
                        animator.SetTrigger("Attack");
                    }
                }
                //}
            }
            timer += Time.deltaTime;
        }
        else
        {
            time += Time.deltaTime;
            //Debug.Log(time);
            if (time > attackTimeOffset)
            {
                attacked = false;
                timer = 0;
                Attack();
            }
        }

    }

    private void KeepDistance()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= stalkingDistance)
        {
            navAgent.speed = 0;
            animator.SetBool("Idling", true);
        }
        else
        {
            navAgent.speed = walkingSpeed;
            animator.SetBool("Idling", false);
        }
    }

    private void changeHealth(int deltaHealth)
    {
        health += deltaHealth;
    }

    private void checkHealth()
    {
        if (health <= 0)
        {
            GameManager.Instance.enemyCount -= 1;
            animator.SetBool("Dead", true);
            GameManager.Instance.score = 0;
            audioSource.PlayOneShot(die, 0.3f);
            Destroy(gameObject, 3);
        }
    }

    private void Attack()
    {
        //Ray ray = new Ray(transform.)
        Debug.Log("We are throwing");
        enemyThrow.Throw_Cannonball();
        audioSource.PlayOneShot(shoot, 0.3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.TryGetComponent(out DestroyCannonball dc))
        {
            changeHealth(-10);
            animator.SetTrigger("Hit");
            stunnedTimer = 0;
            stunned = true;
            navAgent.speed = 0;
        }
    }

    private void checkStunned()
    {
        if (stunned && stunnedTimer >= stunnedDuration)
        {
            stunned = false;
            navAgent.speed = walkingSpeed;
        }
        else if (stunned)
        {
            stunnedTimer += Time.deltaTime;
        }
    }
}

