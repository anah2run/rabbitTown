using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public GameObject prefab;

    public int pooledAmount = 10; // �ʱ⿡ ������ ������Ʈ ��
    GameObject[] pooledObjects; // ��Ȱ��ȭ�� ������Ʈ�� ������ �迭
    int currentIndex = 0; // �������� ����� ������Ʈ�� �ε���

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
        // ���� ����� ������Ʈ�� �������� �ε����� ������ŵ�ϴ�.
        GameObject obj = pooledObjects[currentIndex];
        currentIndex = (currentIndex + 1) % pooledAmount;
        return obj;
    }
}
