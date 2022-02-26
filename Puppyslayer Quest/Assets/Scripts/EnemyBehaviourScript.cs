using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviourScript : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;
    private int locationIndex = 0;
    private NavMeshAgent agent;
    private int _lives = 3;
    private float kinematicWindow = 1.5f; //seconds

    public int EnemyLives
    {
        get
        {
            return _lives;
        }
        private set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "minengeschoss20mm(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("hit");
            if (GetComponent<Rigidbody>().isKinematic == true)
            {
                Debug.Log("kinematic is true");
                GetComponent<Rigidbody>().isKinematic = false;
                agent.Stop();
            }
            //enemyKinematicActive = false;
            //rigidbody.isKinematic = enemyKinematicActive;
            //Debug.Log(rigidbody.isKinematic);
        }
    }
    void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            Debug.Log("reached destination");
                MoveToNextPatrolLocation();
        }
        if (GetComponent<Rigidbody>().isKinematic == false)
        {
            kinematicTimer();
            if (kinematicWindow < 0)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                kinematicWindow = 1.5f;
                agent.Resume();
            }
        }
    }

    void kinematicTimer()
    {
        kinematicWindow -= Time.deltaTime;
    }

    void InitializePatrolRoute()
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
            Debug.Log("Player detected - attack or something");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
}
