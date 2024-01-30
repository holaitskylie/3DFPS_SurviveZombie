using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IItem
{
    private float rotationSpeed = 20f;
    [SerializeField] private float health = 50f;
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        
    }

    public void Use(GameObject target)
    {
        if (target.gameObject.tag == "Player")
        {
            Debug.Log("Pick up" + gameObject.name);
            target.GetComponent<PlayerHealth>().RestoreHealth(health);
            Destroy(gameObject);
        }

    }

    

   
}
