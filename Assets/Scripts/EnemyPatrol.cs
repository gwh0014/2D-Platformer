using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float patrolDistance = 3f;

    Vector3 startingPosition;
    int direction = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * direction * patrolSpeed * Time.deltaTime;
        float distanceFromStart = Vector3.Distance(transform.position, startingPosition);
        if (Mathf.Abs(distanceFromStart) >= patrolDistance)
        {
            direction *= -1;
        }
    }
}
