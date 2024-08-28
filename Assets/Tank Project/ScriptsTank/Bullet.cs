using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] PooledObject pooledObject;
    [SerializeField] float MoveSpeed;
    [SerializeField] float returnTime; // 총알을 풀에 반납하는 데 까지 걸리는 시간

    private float remainTime; // 총알을 생성한 시간

    void OnEnable()
    {
        remainTime = returnTime;
    }

    void Update()
    {
        // 앞으로 이동할 수 있는 기능 구현
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);

        // 일정시간이 지나면, 다시 풀에 반납해줄 수 있는 if문을 작성
        remainTime -= Time.deltaTime;
        if (remainTime < 0) { pooledObject.ReturnPool(); }
    }
}
