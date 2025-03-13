using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject skeleBoy;
    [SerializeField] private GameObject skeleBoyButFar;

    //[SerializeField] private (int, int)[] waveNums;
    [SerializeField] private GameObject[] spawnPoints;

    [SerializeField] private int numFarSkeles;
    [SerializeField] private int numCloseSkeles;

    [SerializeField] private float waveIncreaseModifier;

    private int waveNum = 0;
    //[SerializeField] private EnemyHolder enemyHolder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoints = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++) 
        {
            spawnPoints[i] = gameObject.transform.GetChild(i).gameObject;
        }

        spawnSkeletons();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.enemyCount <= 0) 
        {
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
    }

    private void spawnSkeleton(GameObject skele) 
    {
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //GameObject go = Instantiate(skeleBoy, enemyHolder.transform);
        GameObject go = Instantiate(skele);
        //go.transform.SetParent(enemyHolder.transform);
        go.transform.position = spawnPoint.transform.position;
        go.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
