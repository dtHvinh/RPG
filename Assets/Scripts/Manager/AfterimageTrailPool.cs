using System.Collections.Generic;
using UnityEngine;

public class AfterimageTrailPool : MonoBehaviour
{
    public static AfterimageTrailPool Instance;

    [Header("Pool Settings")]
    public GameObject afterimagePrefab;
    public int poolSize = 20;

    private Queue<GameObject> poolQueue;

    void Awake()
    {
        Instance = this;
        poolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject ghost = Instantiate(afterimagePrefab, transform);
            ghost.SetActive(false);
            poolQueue.Enqueue(ghost);
        }
    }

    public GameObject GetFromPool()
    {
        GameObject ghost = poolQueue.Count > 0 ? poolQueue.Dequeue() : Instantiate(afterimagePrefab, transform);
        ghost.SetActive(true);
        return ghost;
    }

    public void ReturnToPool(GameObject ghost)
    {
        ghost.SetActive(false);
        poolQueue.Enqueue(ghost);
    }
}
