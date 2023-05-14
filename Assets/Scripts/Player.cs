using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;

    private void Update() {
        Vector2 inputDirection = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            inputDirection.y++;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputDirection.x--;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputDirection.y--;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputDirection.x++;
        }

        inputDirection = inputDirection.normalized;

        Vector3 moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);

        transform.position += moveDirection * Time.deltaTime * moveSpeed;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }
}
