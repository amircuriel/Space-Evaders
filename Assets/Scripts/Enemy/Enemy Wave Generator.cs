using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyWaveGenerator : ObjectPool
{
    public float SpawnInterval = 8f;

    public List<Transform> EnemyShipPaths;

    private List<Transform[]> ExpandedPaths;
    private int currentPath = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        ExpandedPaths = new List<Transform[]>();
        base.Start();

        foreach (Transform transform in EnemyShipPaths)
        {
            List<Transform> path = new List<Transform>();
            foreach (Transform child in transform)
            {
                path.Add(child);
            }
            ExpandedPaths.Add(path.ToArray());
        }

        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator EnemySpawner()
    {
        while (true)
        {
            if (SessionManager.Instance.isGameRunning)
            {
                SpawnShip();
                yield return new WaitForSeconds(SpawnInterval);
                for (int i = 0; i < 2; i++)
                {
                    SpawnShip();
                    yield return new WaitForSeconds(1f);
                }
                yield return new WaitForSeconds(SpawnInterval);
                for (int i = 0; i < 3; i++)
                {
                    SpawnShip();
                    yield return new WaitForSeconds(1f);
                }
                yield return new WaitForSeconds(SpawnInterval);
            }
            yield return null;
        }
    }

    private void SpawnShip()
    {
        EnemyController enemyShip = GetPooledObject().GetComponent<EnemyController>();
        if (enemyShip != null)
        {
            enemyShip.waypoints = ExpandedPaths[currentPath];
            enemyShip.gameObject.SetActive(true);

            currentPath++;
            if (currentPath == EnemyShipPaths.Count)
            {
                currentPath = 0;
            }
        }
    }
}
