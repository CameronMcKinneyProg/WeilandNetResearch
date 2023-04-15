using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int maxEnemies = 10;
    public static Dictionary<int, Enemy> enemies = new Dictionary<int, Enemy>();
    private static int nextEnemyId = 1;

    public int id;
    public EnemyState state;
    public Player target;
    public CharacterController controller;
    public Transform shootOrigin;
    public float gravity = -9.81f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 8f;
    public float health;
    public float maxHealth = 100f;
    public float detectionRange = 30f;
    public float shootRange = 15f;
    public float shootAccuracy = 0.1f;
    public float patrolDuration = 3f;
    public float idleDuration = 1f;

    private bool isPatrolRoutineRunning;
    private float yVelocity = 0f;

    private void Start()
    {
        id = nextEnemyId;
        nextEnemyId++;
        enemies.Add(id, this);

        state = EnemyState.patrol;
        gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
        patrolSpeed *= Time.fixedDeltaTime;
        chaseSpeed *= Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.idle:

                break;
            case EnemyState.patrol:

                break;
            case EnemyState.chase:

                break;
            case EnemyState.attack:

                break;
            default:
                break;
        }
    }

    private bool LookForPlayer()
    {
        foreach (Client _client in Server.clients.Values)
        {
            if (_client.player != null)
            {
                Vector3 _enemyToPlayer = _client.player.transform.position - transform.position;
                if (_enemyToPlayer.magnitude <= detectionRange)
                {
                    if (Physics.Raycast(shootOrigin.position, _enemyToPlayer, out RaycastHit _hit, detectionRange))
                    {
                        if (_hit.collider.CompareTag("Player"))
                        {
                            target = _hit.collider.GetComponent<Player>();
                            if (isPatrolRoutineRunning)
                            {
                                isPatrolRoutineRunning = false;
                                // TODO: cancel patrol routine
                            }

                            state = EnemyState.chase;
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private void Patrol()
    {
        if (!isPatrolRoutineRunning)
        {
            StartCoroutine(StartPatrol());
        }
    }

    private IEnumerator StartPatrol()
    {
        isPatrolRoutineRunning = true;
        Vector2 _randomPatrolDirection = Random.insideUnitCircle.normalized;
        transform.forward = new Vector3(_randomPatrolDirection.x, 0f, _randomPatrolDirection.y);

        yield return new WaitForSeconds(patrolDuration);
    }
}

public enum EnemyState
{
    idle,
    patrol,
    chase,
    attack
}
