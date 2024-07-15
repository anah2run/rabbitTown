using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public GameObject prefab;

    public int pooledAmount = 10; // 초기에 생성할 오브젝트 수
    GameObject[] pooledObjects; // 비활성화된 오브젝트를 관리할 배열
    int currentIndex = 0; // 다음으로 사용할 오브젝트의 인덱스

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledObjects = new GameObject[pooledAmount];

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            pooledObjects[i] = obj;
        }
    }

    public GameObject GetPooledObject()
    {
        // 다음 사용할 오브젝트를 가져오고 인덱스를 증가시킵니다.
        GameObject obj = pooledObjects[currentIndex];
        currentIndex = (currentIndex + 1) % pooledAmount;
        return obj;
    }
}
