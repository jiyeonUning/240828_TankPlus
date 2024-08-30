using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] PooledObject prefab;
    [SerializeField] bool createOnEmpty = true;
    [SerializeField] int size;
    [SerializeField] int capacity;

    // 총알을 정한 수 만큼 생성하여 보관해두는 용도의 풀 배열을 생성
    private Stack<PooledObject> pool;

    void Awake()
    {
        // 총알을 담을 풀을 생성
        pool = new Stack<PooledObject>(capacity);

        // 풀에 총알을 생성하는 for문
        for (int i = 0; i < size; i++)
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.Pool = this;
            pool.Push(instance);
        }
    }

    // 풀에 보관한 것을 대여하는 함수
    public PooledObject GetPool(Vector3 position, Quaternion rotation)
    {
        if (pool.Count > 0) // 풀에 보관한 총알의 수가 0일 때,
        {
            PooledObject instance = pool.Pop();
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.gameObject.SetActive(true);
            return instance;
        }
        else if (createOnEmpty) // 총알을 대여하고 있을 때,
        {
            PooledObject instance = Instantiate(prefab);
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.Pool = this;
            return instance;
        }
        else
        {
            return null;
        }
    }

    // 대여한 것을 다시 반납하는 함수
    public void ReturnPool(PooledObject instance)
    {
        // 보관중인 총알의 수가 생성하려는 총알의 수 보다 적을 때, 생성을 멈추는 if문
        if (pool.Count < capacity)
        {
            instance.gameObject.SetActive(false);
            pool.Push(instance);
        }
        else { Destroy(instance.gameObject); }
    }
}
