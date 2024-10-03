using UnityEngine;

internal struct LocalStore
{
    internal static GameObject player;
    internal static Animator playerAnimator;
    internal static StateMachine.GameState gameState;
    internal static StateMachine.AnimationState animationState;
    internal static Vector3 cameraPlace(Transform target, Vector3 offset){
        Vector3 newPosition = target.position + offset;
        return newPosition;
    }
}
