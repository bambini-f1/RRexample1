using UnityEngine.InputSystem;
using UnityEngine;

internal struct StateMachine
{
    internal enum GameState{
        ready,
        game,
        win,
        lose
    }

    internal enum AnimationState{
        idle,
        walk,
        dance
    }

    internal static void PlayerBehaviour(Rigidbody playerBody, GameState gameState, float speedForward, float sideSpeed, InputActionReference actionRef){
        if (gameState == GameState.game){
            Vector2 moveDirection = actionRef.action.ReadValue<Vector2>();
            playerBody.linearVelocity =  new Vector3(moveDirection.x * sideSpeed, playerBody.linearVelocity.y, speedForward);
            float headCorrect = LocalStore.animationState == AnimationState.walk ? 20 : 0;
            Quaternion startPoint = playerBody.transform.rotation;
            Quaternion endPoint = Quaternion.Euler(headCorrect, 0, 0);
            Quaternion smoothTarget = Quaternion.Euler(Quaternion.Lerp(startPoint, endPoint, 0.1f).eulerAngles);
            playerBody.transform.rotation = moveDirection.x == 0 ? smoothTarget : Quaternion.Euler(headCorrect, 45 * moveDirection.x, 0);
        }
        else {
            playerBody.linearVelocity = Vector3.zero;
        }
    }

    internal static void AnimationSense(Animator animator, Rigidbody playerBody, GameState gameState){
        
        animator.SetFloat("Speed", playerBody.linearVelocity.z);
        switch (gameState)
        {
            case GameState.ready:
                LocalStore.animationState = AnimationState.idle;
                break;
            case GameState.game:
                LocalStore.animationState = AnimationState.walk;
                break;
            case GameState.win:
                LocalStore.animationState = AnimationState.dance;
                animator.SetBool("Dance", true);
                break;
        }
    }

}
