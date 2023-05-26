using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


public class EnemyController : MonoBehaviour
{
    [Header("REQUIRED OBJECTS")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject eyes;
    [SerializeField] NavMeshAgent agent;

    [Header("PATROL POINTS")]
    [SerializeField] Transform[] patrol_points;

    [Header("ENEMY STATS")]
    [SerializeField] private float sight_angle;
    [SerializeField] private float base_speed;
    [SerializeField] private float chase_speed;
    [SerializeField] private float sight_range;
    [Space(4)]
    [SerializeField] private float idle_time;
    [SerializeField] private float chasing_time;
    [SerializeField] private float tracking_time;

    private float idle_timer;
    private float chasing_timer;
    private float tracking_timer;

    private state current_state = state.IDLE;
    private bool is_idle = false;
    private bool is_searching = false;
    private bool is_chasing = false;

    enum state
    {
        IDLE,
        PATROLLING,
        INVESTIGATING,
        ALERT,
        CHASING,
        COOLDOWN
    }
    

    private Vector3 getPatrolPts()
    {
        return patrol_points[Random.Range(0, patrol_points.Length-1)].transform.position;
    }

    private Vector3 getClosestPoint()
    {
        Vector3 min_spot = patrol_points[0].transform.position;
        float min = Vector3.Distance(transform.position, patrol_points[0].transform.position);
        float temp;
        //iterate through all searchpoints and get the shortest one to the player
        for (int i = 1; i < patrol_points.Length; i++)
        {
            temp = Vector3.Distance(player.transform.position, patrol_points[i].transform.position);
            if (temp < min)
            {
                min_spot = patrol_points[i].transform.position;
            }
        }

        return min_spot;
    }

    private bool isPlayerVisible()
    {
        //if player enters possible LOS
        if (Vector3.Angle(eyes.transform.forward, (player.transform.position - eyes.transform.position)) <= sight_angle / 2f)
        {
            //checks using raycasting if player is within los
            RaycastHit check;
            if (Physics.Raycast(eyes.transform.position, player.transform.position - eyes.transform.position, out check, sight_range))
            {
                if (check.transform.tag == "Player")
                {
                    Debug.DrawLine(eyes.transform.position, check.transform.position, Color.blue);
                    if (!is_chasing)
                    {
                        current_state = state.ALERT;
                    }
                    else
                    {
                        chasing_timer = chasing_time;
                        tracking_timer = tracking_time;
                        agent.SetDestination(player.transform.position);
                    }

                    return true;
                }
            }
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null || player == null || eyes == null) Debug.LogError("No assigned on enemy NPC");

        idle_timer = idle_time;
        agent.speed = base_speed;
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerVisible();

        switch (current_state)
        {
            //stays still
            case state.IDLE:
                is_chasing = false;
                agent.speed = base_speed;
                if (!is_idle)
                {
                    is_idle = true;
                    idle_timer = idle_time;
                }
                else
                {
                    idle_timer -= Time.deltaTime;
                    if (idle_timer <= 0f)
                    {
                        is_idle = false;
                        current_state = state.PATROLLING;
                    }
                }
                break;
            
            //travelling towards a specific point
            case state.PATROLLING:
                if (!is_searching)
                {
                    agent.SetDestination(getPatrolPts());
                    is_searching = true;
                }
                if (agent.remainingDistance <= 0.1f)
                {
                    current_state = state.IDLE;
                    is_searching = false;
                }
                break;

            //enemy knows player position
            case state.ALERT:
                is_chasing = true;
                agent.speed = chase_speed;
                chasing_timer = chasing_time;
                tracking_timer = tracking_time;

                agent.SetDestination(player.transform.position);
                current_state = state.CHASING;
                break;

            case state.CHASING:
                if (tracking_timer >= 0f)
                {
                    agent.SetDestination(player.transform.position);
                    tracking_timer -= Time.deltaTime;
                }
                else
                {
                    if (Vector3.Distance(transform.position, agent.destination) <= 1f)
                    {
                        agent.SetDestination(getClosestPoint());
                    }
                }

                if (chasing_timer <= 0f)
                {
                    current_state = state.IDLE;
                }
                else
                {
                    chasing_timer -= Time.deltaTime;
                }

                break;
        }

        Debug.DrawLine(transform.position, agent.destination, Color.red);
    }
}
