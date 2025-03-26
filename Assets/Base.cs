using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float velocity;
    Vector3 tarjetPosition;
    Vector3 direction;
    public float speed;
    [SerializeField] GameObject b;
    
   
    void Start()
    {
        
    }
    void LookFollow(Vector3 targetPos,float _speed)
    {
      transform.rotation= Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos),Time.deltaTime*_speed) ;
    }
    void Seek()
    {
        Vector3 desiredVelocity = (b.transform.position- transform.position).normalized * velocity;
        LookFollow(desiredVelocity,2f);
        transform.position = transform.position + transform.forward*Time.deltaTime*speed;
    }
    void Flee()
    {
        Vector3 direction = (transform.position - b.transform.position).normalized * velocity;
        LookFollow(direction, 4f);
        transform.position = transform.position + transform.forward * Time.deltaTime * speed;
    }
    void Persuit()
    {
        Vector3 direction = b.transform.position - transform.position;
        float distance = direction.magnitude;
        float predictionTime = distance / speed;
        Vector3 futurePosition = b.transform.position + (b.transform.forward * predictionTime * speed);
       // Seek(futurePosition);
    }
    void Evade()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //Seek();
        Flee();
    }
}
