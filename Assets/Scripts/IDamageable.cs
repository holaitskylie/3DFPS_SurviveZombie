using UnityEngine;

public interface IDamageable
{
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitnormal);    
}

//damage : 데미지 크기
//hitPoint : 공격당한 위치
//hitNormal : 공격당한 표면의 방향