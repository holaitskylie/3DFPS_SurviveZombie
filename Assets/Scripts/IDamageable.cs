using UnityEngine;

public interface IDamageable
{
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitnormal);    
}

//damage : ������ ũ��
//hitPoint : ���ݴ��� ��ġ
//hitNormal : ���ݴ��� ǥ���� ����