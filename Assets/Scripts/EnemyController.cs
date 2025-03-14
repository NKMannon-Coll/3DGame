using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
public class EnemyController : MonoBehaviour
{

    private NavMeshAgent navAgent;
    private PlayerMovement player;
    private Animator animator;

    private Vector3 positionCorrected;

    [SerializeField] private float walkingSpeed = 1;
    [SerializeField] private float attackDistance = 1;
    [SerializeField] private float attackTimeOffset = 0.3f;
    [SerializeField] private float attackDamage = 10;
    [SerializeField] private float health = 10;
    [SerializeField] private float stunnedDuration = 1.3f;
    [SerializeField] private float visionAngle = 20;

    private float hitDeadTime = 1.333f;
    private float timer = 0;
    private float time = 0;
    private float stunnedTimer = 0;

    private bool attacked = false;
    private bool stunned = false;

    private Ray hit;
    private AudioSource audioSource;
    [SerializeField] private AudioClip slash;
    [SerializeField] private AudioClip die;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<PlayerMovement>();
        animator = GetComponent<Animator>();
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
        navAgent.SetDestination(player.gameObject.transform.position);
        CheckPlayerNear();
        //Debug.Log("help");
    }

    private void CheckPlayerNear() 
    {
        positionCorrected = transform.position + new Vector3(0, 0.4f, 0);
        Debug.DrawLine(positionCorrected, positionCorrected + transform.forward * attackDistance, Color.red);
        if (!attacked)
        {
            if (timer > hitDeadTime)
            {
                timer = 0;
                //if (Physics.Raycast(transform.position, transform.forward * attackDistance, attackDistance))
                //{
                /*if (Physics.Raycast(positionCorrected, transform.forward, out RaycastHit hitInfo, attackDistance))
                {
                    if (hitInfo.transform.gameObject.TryGetComponent(out PlayerMovement pm))
                    {
                        time = 0;
                        attacked = true;
                        //Debug.Log("Yum");
                        animator.SetTrigger("Attack");
                    }
                }*/
                var nearby = (Vector3.Distance(player.transform.position, transform.position)) <= attackDistance;
                var angleToPlayer = player.transform.position - transform.position;
                var inSight = Mathf.Abs(Vector2.Angle(new Vector2(angleToPlayer.x, angleToPlayer.z), new Vector2(transform.forward.x, transform.forward.z))) < visionAngle;
                if (inSight && nearby) 
                {
                    time = 0;
                    attacked = true;
                    //Debug.Log("Yum");
                    animator.SetTrigger("Attack");
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
                Attack();
            }
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
        /*if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, attackDistance))
        {
            if (hitInfo.transform.gameObject.TryGetComponent(out PlayerMovement pm)) 
            {
                GameManager.Instance.changeHealth((int)-attackDamage);
                //Debug.Log(hitInfo.rigidbody.gameObject.name);
            }
            
        }*/

        var nearby = (Vector3.Distance(player.transform.position, transform.position)) <= attackDistance;
        var angleToPlayer = player.transform.position - transform.position;
        var inSight = Mathf.Abs(Vector2.Angle(new Vector2(angleToPlayer.x, angleToPlayer.z), new Vector2(transform.forward.x, transform.forward.z))) < visionAngle;
        if (inSight && nearby)
        {
            GameManager.Instance.changeHealth((int)-attackDamage);
        }
        audioSource.PlayOneShot(slash, 0.3f);
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
