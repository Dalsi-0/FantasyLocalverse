using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class RepeatBG : MonoBehaviour
{
    [SerializeField][Range(1f, 200f)] float speed = 3f;

    [SerializeField] private float posValue;

    private Vector2 startPos;
    private float newPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        newPos = Mathf.Repeat(Time.time * (speed * 0.01f), posValue);
        transform.position = startPos + Vector2.left * newPos;
    }
}
