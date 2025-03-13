using UnityEngine;

public class EnemyThrow : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public float throwCooldown;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private GameObject player;

    private void Start()
    {
        readyToThrow = true;
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    private void Update()
    {

    }

    public void Throw_Cannonball()
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow);
        projectile.transform.position = attackPoint.position;

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        //Vector3 forceDirection = (player.transform.position - attackPoint.position).normalized;
        Vector3 forceDirection = (player.transform.position - attackPoint.position).normalized;

        Debug.DrawLine(transform.position, transform.position + forceDirection, Color.red, 4);

        RaycastHit hit;

        /*if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }*/

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
