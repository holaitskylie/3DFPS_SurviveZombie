using System;
using UnityEngine;

//<summary>
//생명체로 동작하는 게임 오브젝트가 상속 받는 클래스
//</summary>
public class Entity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100f;
    public float currentHealth;
    public bool isDead { get; protected set; }
    public System.Action onDeath;

    protected virtual void OnEnable()
    {
        //게임 오브젝트 활성화 시 초기화 작업 진행

        //사망하지 않은 상태로 설정
        //HP 값 설정
        isDead = false;
        currentHealth = startingHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {       
        //전달 받은 damage만큼 Hp 값 감소
        currentHealth -= damage;

        //현재 hp가 0 보다 같거나 작으며 아직 사망상 태가 아니라면 Die() 호출
        if (currentHealth <= 0 && !isDead)
            Die();

    }    

    public virtual void Die()
    {
        //사망 이벤트가 있으면 이벤트 실행
        if (onDeath != null)
            onDeath();

        //사망 상태로 변경
        isDead = true;
    }
}
