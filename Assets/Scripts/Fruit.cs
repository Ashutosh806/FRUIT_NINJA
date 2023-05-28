using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole,sliced;
    Rigidbody fruitRb;
    Collider fruitColl;
    ParticleSystem juiceParticleEffect;
    public int point=1;

    private void Awake() 
    {
       fruitRb = GetComponent<Rigidbody>(); 
       fruitColl = GetComponent<Collider>();   
       juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    void Slice(Vector3 direction,Vector3 position,float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore(point);
        whole.SetActive(false);
        sliced.SetActive(true);

        fruitColl.enabled = true;
        juiceParticleEffect.Play();
        float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f,0f,angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody slice in slices)
        {
           slice.velocity = fruitRb.velocity;
           slice.AddForceAtPosition(direction*force,position,ForceMode.Impulse);
        }
    }
    
    void OnTriggerEnter(Collider other) 
    {
       if(other.CompareTag("Player"))
       {
        Blade blade = other.GetComponent<Blade>();
         Slice(blade.direction,blade.transform.position,blade.sliceForce);
       } 
    }
}
