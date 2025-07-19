using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f; // 이동 속도
    [SerializeField]
    public GameObject RestartPannel;

    private void Update()
    {
        PlayerCharacterMovement();
        CheckRestart();
    }

    private void PlayerCharacterMovement()
    {
        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) moveDir += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) moveDir += Vector3.right;
        if (Input.GetKey(KeyCode.UpArrow)) moveDir += Vector3.up;
        if (Input.GetKey(KeyCode.DownArrow)) moveDir += Vector3.down;
        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
    }

    private void CheckRestart()
    {
        if (RestartPannel.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            { 
                GameHelper.RestartGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameHelper.RestartGame();
        }
        else if (other.CompareTag("Goal"))
        {
            GameHelper.GameClear(RestartPannel);
        }
    }
}