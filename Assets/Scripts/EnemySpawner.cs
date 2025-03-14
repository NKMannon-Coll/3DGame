using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject skeleBoy;
    [SerializeField] private GameObject skeleBoyButFar;

    //[SerializeField] private (int, int)[] waveNums;
    [SerializeField] private GameObject[] spawnPoints;

    [SerializeField] private int numFarSkeles;
    [SerializeField] private int numCloseSkeles;

    [SerializeField] private float waveIncreaseModifier;

    private bool spawning = true;

    private GameObject spawnLocations;
    private GameObject enemies;

    private int waveNum = 0;
    //[SerializeField] private EnemyHolder enemyHolder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnLocations = gameObject.transform.GetChild(0).gameObject;
        enemies = gameObject.transform.GetChild(1).gameObject;
        spawnPoints = new GameObject[spawnLocations.transform.childCount];
        for (int i = 0; i < spawnLocations.transform.childCount; i++) 
        {
            spawnPoints[i] = spawnLocations.transform.GetChild(i).gameObject;
        }

        spawnSkeletons();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.transform.childCount <= 0 && !spawning) 
        {
            Debug.Log("Why are you here");
            spawnSkeletons();
        }
    }


    private void spawnSkeletons() 
    {
        /*if (waveNum < waveNums.Length)
        {
            for (int i = 0; i < waveNums[0].Item1; i++)
            {
                spawnSkeleton(skeleBoy);
            }
            for (int i = 0; i < waveNums[0].Item2; i++)
            {
                spawnSkeleton(skeleBoyButFar);
            }
        }
        else 
        {

        }*/
        spawning = true;
        int far = (int)(numFarSkeles * waveIncreaseModifier * waveNum);
        int close = (int)(numCloseSkeles * waveIncreaseModifier * waveNum);

        for (int i = 0; i < close; i++)
        {
            spawnSkeleton(skeleBoy);
        }
        for (int i = 0; i < far; i++)
        {
            spawnSkeleton(skeleBoyButFar);
        }

        waveNum++;
        spawning = false;
    }

    private void spawnSkeleton(GameObject skele) 
    {
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //GameObject go = Instantiate(skeleBoy, enemyHolder.transform);
        GameObject go = Instantiate(skele, enemies.transform);
        //go.transform.SetParent(enemyHolder.transform);
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(spawnPoint.transform.position, out closestHit, 500, 1))
        {
            go.transform.position = closestHit.position;
            go.AddComponent<NavMeshAgent>();
            NavMeshAgent nm = go.GetComponent<NavMeshAgent>();
            nm.baseOffset = 0.2f;
        }
        go.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
