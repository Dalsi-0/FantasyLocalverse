using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BridController : MonoBehaviour
{
    [SerializeField] float jumpPower = 3f;
    [SerializeField] float forwardPower = 2f;
    Vector2 jumpDir = Vector2.zero;

    Brid_GameManager brid_GameManager;
    Rigidbody2D myRigidbody2D;

    private void Awake()
    {
        brid_GameManager = GameObject.Find("BirdGameManager").transform.GetComponent<Brid_GameManager>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        jumpDir = new Vector2(forwardPower, jumpPower);
    }

    private void FixedUpdate()
    {
        float angle = Mathf.Clamp(myRigidbody2D.velocity.y * 10f, -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Jump()
    {
        myRigidbody2D.AddForce(jumpDir, ForceMode2D.Impulse);
    }

    public void SetRigidbodyGravityScale(float value)
    {
        myRigidbody2D.gravityScale = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            brid_GameManager.ChangeSystemState(EMiniGameBridState.GameOver);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            brid_GameManager.PlusScore();
        }
    }
}
