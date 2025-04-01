using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereTest : MonoBehaviour
{
    float velocity = 4;
    [SerializeField] GameObject _spot;
    float speed = 5;
   // public Vector3 TargetVelocity { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void LookFollow(Vector3 targetPos, float _speed)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos), Time.deltaTime * _speed);
    }
    // Update is called once per frame
    void Update()
    {
         MoveToPoint2(_spot.transform.position);
        //transform.position += transform.forward * speed * Time.deltaTime;

    }
    void MoveToPoint(Vector3 targetPoint)
    {
        Vector3 desiredVelocity = (targetPoint - transform.position).normalized * velocity;


        LookFollow(desiredVelocity, 2f);


        transform.position += transform.forward * Time.deltaTime * speed;
    }
    void MoveToPoint2(Vector3 targetPoint)
    {
        float stopDistance = 0.8f; // Distancia mínima para detenerse

        // Calcular la distancia al objetivo
        float distance = Vector3.Distance(transform.position, targetPoint);

        if (distance > stopDistance) // Solo moverse si está lejos
        {
            Vector3 desiredVelocity = (targetPoint - transform.position).normalized * velocity;

            LookFollow(desiredVelocity, 2f);
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }

}
