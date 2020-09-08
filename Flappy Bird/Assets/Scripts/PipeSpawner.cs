using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float height;
    public GameObject pipes;
    private IEnumerator coroutineFunc;
    public float spawnTime;
    void Start()
    {
        coroutineFunc = spawnObject(spawnTime);
        StartCoroutine(coroutineFunc);
    }

    public IEnumerator spawnObject(float spawnTime)
    {
        while (!BirdControl.isDead)
        {
            Instantiate(pipes, new Vector3(2, Random.Range(-height, height), 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }

    }
    
}
