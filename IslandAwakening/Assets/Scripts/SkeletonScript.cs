using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SkeletonAI : MonoBehaviour
{
    public enum State { Wander, Chase, Attack }
    [Header("References")]
    public Transform player;
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Wander Settings")]
    public float wanderRadius = 10f;
    public float idleTimeMin = 2f;
    public float idleTimeMax = 5f;

    [Header("Chase/Attack Settings")]
    public float chaseRange = 15f;
    public float attackRange = 2.5f;
    public float attackCooldown = 1.5f;
    public float attackDamage = 10f;
    public float attackHitDelay = 0.5f;  // time in seconds into the animation when the “hit” actually lands

    State currentState = State.Wander;
    float nextAttackTime = 0f;

    void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        StartCoroutine(StateLoop());
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            // Decide which state to be in
            if (dist <= attackRange && Time.time >= nextAttackTime)
                currentState = State.Attack;
            else if (dist <= chaseRange)
                currentState = State.Chase;
            else
                currentState = State.Wander;

            // Run behavior for current state
            switch (currentState)
            {
                case State.Wander:
                    yield return StartCoroutine(Wander());
                    break;
                case State.Chase:
                    yield return StartCoroutine(Chase());
                    break;
                case State.Attack:
                    yield return StartCoroutine(Attack());
                    break;
            }

            yield return null;
        }
    }

    IEnumerator Wander()
    {
        // Idle before moving
        animator.SetBool("isMoving", false);
        float idle = Random.Range(idleTimeMin, idleTimeMax);
        yield return new WaitForSeconds(idle);

        // Pick random point
        Vector3 randomDir = Random.insideUnitSphere * wanderRadius;
        randomDir += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            animator.SetBool("isMoving", true);
            // Wait until agent arrives or times out
            float start = Time.time;
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                if (Time.time - start > 10f) break; // fail-safe
                yield return null;
            }
        }

        animator.SetBool("isMoving", false);
        yield return null;
    }

    IEnumerator Chase()
    {
        agent.stoppingDistance = attackRange * 0.9f;
        agent.SetDestination(player.position);
        animator.SetBool("isMoving", true);

        // Keep chasing until out of chaseRange
        while (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            agent.SetDestination(player.position);
            yield return null;
        }

        animator.SetBool("isMoving", false);
        yield return null;
    }

    IEnumerator Attack()
    {
        // Face the player
        Vector3 dir = (player.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = lookRot;

        // Trigger attack
        animator.SetTrigger("Attack");
        nextAttackTime = Time.time + attackCooldown;

            // wait until the “bone throw” / hit actually occurs in the animation
        yield return new WaitForSeconds(attackHitDelay);

        // if the player is still in range, apply damage
        if (Vector3.Distance(transform.position, player.position) <= attackRange + 0.1f)
        {
            PlayerState.Instance.TakeDamage(attackDamage);
        }

        // now wait out the rest of the cooldown
        yield return new WaitForSeconds(attackCooldown - attackHitDelay);
    }
}
