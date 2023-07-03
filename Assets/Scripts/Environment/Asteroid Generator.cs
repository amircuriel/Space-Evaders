using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : ObjectPool
{
    public float SpawnInterval = 1f;
    public float SpawnRadius = 5.54f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartCoroutine(AsteroidSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AsteroidSpawner()
    {
        while (true)
        {
            if (SessionManager.Instance.isGameRunning)
            {
                GameObject asteroid = GetPooledObject();
                if (asteroid != null)
                {
                    Vector2 randomSpawnPoint = Random.insideUnitCircle.normalized * SpawnRadius;
                    asteroid.transform.position = randomSpawnPoint;

                    Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, Vector2.zero - randomSpawnPoint);
                    toRotation.eulerAngles += new Vector3(0, 0, Random.Range(-25, 25));
                    asteroid.transform.rotation = toRotation;
                    asteroid.SetActive(true);
                }

                yield return new WaitForSeconds(SpawnInterval);
            }
            yield return null;
        }
    }
}
