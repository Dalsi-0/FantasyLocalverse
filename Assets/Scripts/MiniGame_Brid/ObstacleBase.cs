using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleBase : MonoBehaviour
{
    [SerializeField][Range(1f, 200f)] float speed = 3f;
    [SerializeField] bool isGround;
    [SerializeField] float widthPadding = 2.5f;
    Vector3 lastPosition;
    Brid_GameManager brid_GameManager;
    Transform highObstacle;
    Transform underObstacle;

    private void Awake()
    {
        brid_GameManager = GameObject.Find("BirdGameManager").transform.GetComponent<Brid_GameManager>();

        if (!isGround)
        {
            highObstacle = transform.GetChild(0);
            underObstacle = transform.GetChild(1);
            SetVerticalWidth();
        }
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void ResetPosition()
    {
        lastPosition = GetRightmostObstaclePosition(isGround);
        if(!isGround)
        {
            SetVerticalWidth();
        }

        Vector3 newPosition = lastPosition + new Vector3(widthPadding, 0, 0);
        transform.position = newPosition;
    }
    Vector3 GetRightmostObstaclePosition(bool isGround)
    {
        Vector3 rightmostPosition = Vector3.zero;
        if (isGround)
        { 
            rightmostPosition = brid_GameManager.grounds[0].transform.position;

            foreach (GameObject obstacle in brid_GameManager.grounds)
            {
                if (obstacle.transform.position.x > rightmostPosition.x)
                {
                    rightmostPosition = obstacle.transform.position;
                }
            }
        }
        else 
        {  
            rightmostPosition = brid_GameManager.obstacles[0].transform.position;

            foreach (GameObject obstacle in brid_GameManager.obstacles)
            {
                if (obstacle.transform.position.x > rightmostPosition.x)
                {
                    rightmostPosition = obstacle.transform.position;
                }
            }
            SetVerticalWidth();
        }

        return rightmostPosition;
    }

    void SetVerticalWidth()
    {
        float minGap = 3f;
        float maxGap = 4f; 
        float maxHeightOffset = 1.0f;

        float randomGap = Random.Range(minGap, maxGap);

        float centerY = Random.Range(-maxHeightOffset, maxHeightOffset);

        highObstacle.localPosition = new Vector3(highObstacle.localPosition.x, centerY + (randomGap / 2), highObstacle.localPosition.z);
        underObstacle.localPosition = new Vector3(underObstacle.localPosition.x, centerY - (randomGap / 2), underObstacle.localPosition.z);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ResetCollider"))
        {
            ResetPosition();
        }
    }
}
