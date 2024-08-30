using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] ObjectPool[] bulletPool; // 총알의 종류 생성
    private ObjectPool curBulletPool; 
    [SerializeField] Transform muzzlePoint; // 총알을 생성할 위치

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    private void Awake()
    {
        // 초기 총알의 설정을 1번으로 고정하였다.
        curBulletPool = bulletPool[0];
    }

    void Update()
    {
        // 탱크 움직임 구현
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.forward * z * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, x * rotateSpeed * Time.deltaTime);

        // 스페이스바를 통한 총알 발사
        if (Input.GetKeyDown(KeyCode.Space)) { Fire(); }

        // 1, 2, 3번을 통해 총알을 교체해주는 if문
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SwapBullet(0); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { SwapBullet(1); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { SwapBullet(2); }
    }


    void Fire()
    {
        // 선택한 총알을 muzzlePoint에서 발포한다.
        curBulletPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
    }

    void SwapBullet(int num)
    {
        // 하나의 종류를 배열 하나에 넣는 것으로 총알의 종류를 구분해줄 수 있다.
        curBulletPool = bulletPool[num];
    }
}
