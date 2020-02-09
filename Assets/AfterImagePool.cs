using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour {

    [SerializeField]
    private GameObject afterimageprefab;

    private Queue<GameObject> availableobjects = new Queue<GameObject>();

    public static AfterImagePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for(int i=0; i<10; i++)
        {
            var instancetoAdd = Instantiate(afterimageprefab);
            instancetoAdd.transform.SetParent(transform);
            AddToPool(instancetoAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableobjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if(availableobjects.Count==0)
        {
            GrowPool();
        }

        var instance = availableobjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
