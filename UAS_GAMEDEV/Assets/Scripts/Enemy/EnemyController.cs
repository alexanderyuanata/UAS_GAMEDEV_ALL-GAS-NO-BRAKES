using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    [Header("REQUIRED OBJECTS")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject eyes;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    [Header("PATROL POINTS")]
    [SerializeField] Transform[] patrol_points;

    [Header("ENEMY STATS")]
    [SerializeField] private float sight_angle;
    [SerializeField] private float base_speed;
    [SerializeField] private float chase_speed;
    [SerializeField] private float sight_range;
    [SerializeField] private float hearing_range;
    [Space(10)]
    [SerializeField] private float idle_time;
    [SerializeField] private float chasing_time;
    [SerializeField] private float tracking_time;
    [Space(10)]
    [SerializeField] private float attack_range;

    [Header("STEALTH STATS")]
    [SerializeField] private float alert_threshold;
    [SerializeField] private float run_alert;
    [SerializeField] private float walk_alert;
    [SerializeField] private float crouch_alert;
    [SerializeField] private float hearing_alert;
    [SerializeField] private float alert_decay;
    [SerializeField]
    private float alert_amount = 0;

    [Header("MUSIC & SFX")]
    [SerializeField] private AudioSource scream_source;
    [SerializeField] private AudioSource footstep_source;
    [SerializeField] private AudioSource heartbeat_source;
    [SerializeField] private AudioSource bite_source;

    private float idle_timer;
    private float chasing_timer;
    private float tracking_timer;

    private state current_state = state.IDLE;
    private bool is_idle = false;
    private bool is_searching = false;
    private bool is_chasing = false;
    private bool is_investigating = false;
    private Vector3 investigation_loc;
    private float alert_multiplier = 1f;

    private bool on = false;

    public void setOn(bool b)
    {
        on = b;
    }

    enum state
    {
        IDLE,
        PATROLLING,
        INVESTIGATING,
        ALERT,
        CHASING,
    }
    
    private void triggerGameOver()
    {
        GameManager.instance.gameOver();
    }

    private void Awake()
    {
        instance = this;
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

    private void StartBiteSound()
    {
        if (scream_source.isPlaying) scream_source.Stop();
        bite_source.Play();
    }

    public void Investigate(Vector3 loc)
    {
        investigation_loc = loc;
        current_state = state.INVESTIGATING;
    }

    private void Alerted()
    {
        if (!is_chasing)
        {
            current_state = state.ALERT;
            scream_source.Play();
        }
        else
        {
            if (isPlayerVisible() && Vector3.Distance(eyes.transform.position, player.transform.position) < attack_range)
            {
                PlayerDeath.instance.triggerDeathCam();
                agent.isStopped = true;
                
                animator.SetTrigger("kill");
            }
            else
            {
                chasing_timer = chasing_time;
                tracking_timer = tracking_time;
                agent.SetDestination(player.transform.position);
            }
        }
    }

    private bool isPlayerVisible()
    {
        if (Vector3.Angle(eyes.transform.forward, (player.transform.position - eyes.transform.position)) <= sight_angle / 2f)
        {
            Debug.DrawRay(eyes.transform.position, player.transform.position - eyes.transform.position, Color.red);
            RaycastHit check;
            if (Physics.Raycast(eyes.transform.position, player.transform.position - eyes.transform.position, out check, sight_range))
            {
                if (check.transform.tag == "Player")
                {
                    Debug.DrawLine(eyes.transform.position, check.transform.position, Color.blue);
                    return true;
                }
            }
        }

        return false;
    }

    public float getAlertAmount()
    {
        return alert_amount;
    }

    public float getMaxAlert()
    {
        return alert_threshold;
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
        if (!on) return;
        if (PlayerMovement.instance.isRunning() && Vector3.Distance(transform.position, player.transform.position) <= hearing_range)
        {
            if (!is_chasing)
            {
                is_investigating = false;
                Investigate(player.transform.position);
            }
            else
            {
                Alerted();
            }
        }

        if (current_state != state.CHASING && current_state != state.ALERT && !isPlayerVisible() && alert_amount > 0f)
        {
            alert_amount -= alert_decay * Time.deltaTime;
        }

        if (isPlayerVisible() && !is_chasing)
        {
            switch (PlayerMovement.instance.getMoveState())
            {
                case PlayerMovement.movingStates.RUNNING:
                    alert_amount += run_alert * Time.deltaTime;
                    break;

                case PlayerMovement.movingStates.WALKING:
                    alert_amount += walk_alert * Time.deltaTime;
                    break;

                case PlayerMovement.movingStates.CROUCHING:
                    alert_amount += crouch_alert * Time.deltaTime;
                    break;
            }
        }
        else if (isPlayerVisible() && is_chasing)
        {
            Alerted();
        }

        if (alert_amount >= alert_threshold && !is_chasing)
        {
            Alerted();
        }

        alert_amount = Mathf.Clamp(alert_amount, 0f, alert_threshold);

        animator.SetBool("walking", agent.velocity.magnitude > 0.1f);
        animator.SetBool("chasing", is_chasing);

        switch (current_state)
        {
            //stays still
            case state.IDLE:
                is_chasing = false;
                is_investigating = false;
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
                else if (agent.remainingDistance <= 0.1f)
                {
                    current_state = state.IDLE;
                    is_searching = false;
                }
                break;

            case state.INVESTIGATING:
                if (!is_investigating)
                {
                    agent.SetDestination(investigation_loc);
                    is_investigating = true;
                }
                else if (agent.remainingDistance <= 0.1f)
                {
                    is_investigating = false;
                    is_searching = false;
                    current_state = state.PATROLLING;
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

            //enemy trying to chase player
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
