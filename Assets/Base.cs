using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Base : MonoBehaviour
{
    public float velocity;
    Vector3 tarjetPosition;
    Vector3 direction;
    public float speed;
    [SerializeField] GameObject b;

    //float returnedValue;
    float timer = 0f;
    [SerializeField] private List<GameObject> waypoints;
    private int currentWaypointIndex = 0;
    public float waypointThreshold = 1.0f;

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
       if (speed <= 0) return;
       float predictionTime = distance / speed;
       tarjetPosition = b.transform.position + (b.transform.forward * predictionTime * velocity);
       Vector3 desiredVelocity = (tarjetPosition - transform.position).normalized * velocity;
       LookFollow(desiredVelocity, 2f);
       transform.position += transform.forward * Time.deltaTime * speed;


    }
    void Evade()
    {
            Vector3 direction = b.transform.position - transform.position;
            float distance = direction.magnitude;
            if (speed <= 0) return;
        float predictionTime = distance / (speed + velocity * 0.5f);

        Vector3 futurePosition = b.transform.position + (b.transform.forward * predictionTime * velocity);
            Vector3 fleeDirection = (transform.position - futurePosition).normalized * velocity;
            LookFollow(fleeDirection, 2f);
            transform.position += transform.forward * Time.deltaTime * speed;
       //maomeno

    }
    void Wander()
    {
        timer += Time.deltaTime; 

        if (timer >= 1f) 
        {
            timer = 0f;

            float x = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            Vector3 randomDir = new Vector3(x, 0, z).normalized;

            transform.rotation = Quaternion.LookRotation(randomDir);
        }
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    void PathFollowing()
    {
        if (waypoints.Count == 0) return;
        Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;
        Vector3 desiredVelocity = (targetPosition - transform.position).normalized * velocity;
        LookFollow(desiredVelocity, 2f);
        transform.position += transform.forward * Time.deltaTime * speed;
        if (Vector3.Distance(transform.position, targetPosition) < waypointThreshold)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
        }
    }
    void Update()
    {
        //Seek();
        //Flee();
        //  Persuit();
        Evade();
       // Wander();
       // PathFollowing();
        //ObstacleAvoidance();
        //Arrive();
    }
    void Arrive()
    {
        float slowingDistance = 3f;
        float distance = Vector3.Distance(transform.position, b.transform.position);
        float rampedSpeed = speed * (distance / slowingDistance);
        float clippedSpeed = Mathf.Min(rampedSpeed, speed);
        Vector3 desiredVelocity = (b.transform.position - transform.position).normalized * clippedSpeed;
        LookFollow(desiredVelocity, 5f);
        transform.position += desiredVelocity * Time.deltaTime;

    }
    void ObstacleAvoidance()
    {
        float detectionDistance = 3.5f;
        Debug.DrawLine(transform.position, transform.position + transform.forward * detectionDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, detectionDistance))
        {
            if (!Physics.Raycast(transform.position, -transform.right, detectionDistance))
            {
                Debug.DrawLine(transform.position, transform.position - transform.right * detectionDistance, Color.green);
                transform.Rotate(0, -45, 0);
            }
            else if (!Physics.Raycast(transform.position, transform.right, detectionDistance))
            {
                Debug.DrawLine(transform.position, transform.position + transform.right * detectionDistance, Color.green);
                transform.Rotate(0, 45, 0);
            }
            else
            {
                transform.Rotate(0, 180, 0);
            }
        }
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
