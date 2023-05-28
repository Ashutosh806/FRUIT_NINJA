using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
   Collider spawnArea;
   public GameObject[] fruitPrefab;
   public GameObject bombPrefab;
   [Range(0f,1f)]
   public float bombChance = 0.05f ;
   public float minDelay = 0.25f,maxDelay = 1f;

   public float minAngle = -15f,maxAngle = 15f;

   public float minForce = 18f,maxForce = 22f;

   public float maxLifetime = 5f;
   void Awake() 
   {
    spawnArea = GetComponent<Collider>();
   }

   void OnEnable() 
   {
    StartCoroutine(Spawn());   
   }

   void OnDisable() 
   {
    StopAllCoroutines();
   }

   private IEnumerator Spawn()
   {
     yield return new WaitForSeconds(2f);
     while(enabled)
     {
        GameObject prefab = fruitPrefab[Random.Range(0,fruitPrefab.Length)];
        if(Random.value < bombChance)
        {
          prefab = bombPrefab;
        }
        Vector3 position = new Vector3();
        position.x = Random.Range(spawnArea.bounds.min.x,spawnArea.bounds.min.x);
        position.y = Random.Range(spawnArea.bounds.min.y,spawnArea.bounds.min.y);
        position.z = Random.Range(spawnArea.bounds.min.z,spawnArea.bounds.min.z);

        Quaternion rotation = Quaternion.Euler(0f,0f,Random.Range(minAngle,maxAngle));
        GameObject fruit = Instantiate(prefab,position,rotation);
        Destroy(fruit,maxLifetime);

        float force = Random.Range(minForce,maxForce);
        fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up*force, ForceMode.Impulse);

        yield return new WaitForSeconds(Random.Range(minDelay,maxDelay));
     }
   }
}
