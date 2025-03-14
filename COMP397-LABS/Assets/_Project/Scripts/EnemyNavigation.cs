using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer397
{
    public class EnemyNavigation : MonoBehaviour, IObserver
    {

        private NavMeshAgent agent;
        [SerializeField] private PlayerController player;
        [SerializeField] private List<Transform> waypoints = new List<Transform>();
        [SerializeField] private float distanceThreshold = 1.0f;
        private int index = 0;
        private Vector3 destination;
        
        // Enemy Sensing Stats
        [SerializeField] private LayerMask mask; // That layer that correspond to player
        [SerializeField] private int viewDistance = 10;
        [SerializeField] private EnemyStates state = EnemyStates.Patrolling; // Initial state

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            destination = waypoints[index].position;
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            player.AddObserver(this);
        }

        private void OnDisable()
        {
            player.RemoveObserver(this);
        }

        private void Start()
        {
            agent.destination = destination;
        }

        private void Update()
        {
            switch (state)
            {
                // Patrol while the player is not visible
                case EnemyStates.Patrolling:
                    if (Vector3.Distance(destination, transform.position) < distanceThreshold)
                    {
                        index = (index + 1) % waypoints.Count;
                        destination = waypoints[index].position;
                    }
                    break;
                case EnemyStates.Chasing:
                    // Chasing the player while player is visible
                    destination = player.gameObject.transform.position;
                    break;
                default:
                    Debug.LogError("State not configured", this);
                    break;
            }
            agent.destination = destination;
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                    viewDistance, mask))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    state = EnemyStates.Chasing;
                }
                Debug.Log("Hit" + hit.transform.gameObject.name);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            }
            else 
            {
                state = EnemyStates.Patrolling;
                Debug.Log("Hit Nothing");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * viewDistance, Color.yellow);
            }
        }

        private void OnDrawGizmos()
        {
            if (waypoints.Count > 0)
            {
                Gizmos.color = Color.magenta;
                for (int i = 0; i < waypoints.Count; i++)
                {
                    Gizmos.DrawSphere(waypoints[i].position, distanceThreshold);
                    if (i > 0)
                    {
                        Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
                    }
                }
            }
        }

        public void OnNotify()
        {
            Debug.Log($"Notified by the Subject");
        }
    }
}
