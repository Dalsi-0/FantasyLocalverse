using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimator : MonoBehaviour
{
    [SerializeField] private Transform centerPoint; 
    [SerializeField] private float moveRadius = 3f; 
    [SerializeField] private float speed = 2f;
    private Vector2 targetPosition;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
        SetNewTarget();
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position.x > lastPosition.x)
        {
            spriteRenderer.flipX = false; 
        }
        else if (transform.position.x < lastPosition.x)
        {
            spriteRenderer.flipX = true; 
        }

        lastPosition = transform.position;

        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            SetNewTarget();
        }
    }
    void SetNewTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * moveRadius; 
        targetPosition = (Vector2)centerPoint.position + randomOffset;
    }
}
