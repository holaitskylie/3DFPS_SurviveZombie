using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour, IItem
{
    private float rotationSpeed = 20f;
    [SerializeField] private int ammoAmount = 5;
    [SerializeField] private AmmoType ammoType;

    private void Update()
    {
        transform.GetChild(0).gameObject.transform.
            Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Pick up" + ammoType + " : " + ammoAmount);
            //FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            other.GetComponent<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }*/
    
    public void Use(GameObject target)
    {
        if(target.gameObject.tag == "Player")
        {
            Debug.Log("Pick up" + ammoType + " : " + ammoAmount);
            target.GetComponent<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
        
    }
}
