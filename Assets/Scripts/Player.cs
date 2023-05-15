using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float radius = .7f;
    [SerializeField] private float height = 2f;

    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void Update() {
        ResolveMoving(
            playerInputActions.Player.Move.ReadValue<Vector2>().normalized
        );
    }

    private void ResolveMoving(
        Vector2 inputDirection
    ) {
        Vector3 capsulePoint1 = transform.position;
        Vector3 capsulePoint2 = transform.position + Vector3.up * height;
        float moveDistance = Time.deltaTime * moveSpeed;

        // check an initial intention to move
        Vector3 initialMoveDirection = new Vector3(
            inputDirection.x, 0, inputDirection.y
        );
        Vector3 finalMoveDirection = initialMoveDirection;
        if (
            !CheckMove(
                capsulePoint1,
                capsulePoint2,
                initialMoveDirection,
                moveDistance
            )
        ) {
            // check x
            finalMoveDirection = new Vector3(
                initialMoveDirection.x,
                0,
                0
            ).normalized;
            if (
                !CheckMove(
                    capsulePoint1,
                    capsulePoint2,
                    finalMoveDirection,
                    moveDistance
                )
            ) {
                // check z
                finalMoveDirection = new Vector3(
                    0,
                    0,
                    initialMoveDirection.z
                ).normalized;
                if (
                    !CheckMove(
                        capsulePoint1,
                        capsulePoint2,
                        finalMoveDirection,
                        moveDistance
                    )
                ) {
                    return;
                }
            }
        }

        ApplyMove(
            finalMoveDirection,
            moveDistance
        );
    }

    /// <summary>
    /// Applies a move direcion and a move distance to the current position.
    /// </summary>
    private void ApplyMove(
        Vector3 moveDirection,
        float moveDistance
    ) {
        transform.position += moveDirection * moveDistance;
        transform.forward = Vector3.Slerp(
            transform.forward, moveDirection, moveDistance
        );
    }

    private bool CheckMove(
        Vector3 point1,
        Vector3 point2,
        Vector3 moveDirection,
        float moveDistance
    ) {
        return !Physics.CapsuleCast(
            point1,
            point2,
            radius,
            moveDirection,
            moveDistance
        );
    }
}
