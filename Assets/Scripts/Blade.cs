using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    Collider bladeColl;
    Camera mainCamera;
    TrailRenderer bladeTrail;
    bool slicing;
    [SerializeField] float minVelocity = 0.01f;
    public Vector3 direction{get;private set;}
    public float sliceForce = 5f;
    

    void Awake() 
    {
      mainCamera = Camera.main;
      bladeColl = GetComponent<Collider>();   
      bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    void OnEnable() 
    {
      StopSlicing();  
    }

    void OnDisable() 
    {
       StopSlicing();  
    }

    void Update()
    {
     if(Input.GetMouseButtonDown(0)) 
     {
        StartSlicing();
     }  
     else if(Input.GetMouseButtonUp(0))
     {
        StopSlicing();
     }
     else if(slicing)
     {
        ContinueSlicing();
     }

     if(Input.GetKey(KeyCode.Escape))
      {
        Application.Quit();
      }  
    }

    void StartSlicing()
    {
      Vector3 newPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      newPos.z = 0f;
      transform.position = newPos;

      slicing = true;
      bladeColl.enabled=true;
      bladeTrail.enabled = true;
      bladeTrail.Clear();
    }
    void StopSlicing()
    {
      slicing = false;
      bladeColl.enabled=false;
      bladeTrail.enabled = false;
    }
    void ContinueSlicing()
    {
     Vector3 newPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
     newPos.z = 0f;

     direction = newPos - transform.position;
     float velocity = direction.magnitude/Time.deltaTime;
     bladeColl.enabled = velocity > minVelocity;
     transform.position = newPos;
    }
}
