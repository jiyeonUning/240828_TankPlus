using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool pool;
    public ObjectPool Pool
    { get { return pool; } set { pool = value; } }

    public void ReturnPool()
    {
        if (pool != null) { pool.ReturnPool(this); }
        else { Destroy(gameObject); }
    }
}
