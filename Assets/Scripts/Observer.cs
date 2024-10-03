using UnityEngine.InputSystem;
using UnityEngine;

internal class Observer : MonoBehaviour
{
    #region SerialazeField's
    
    [SerializeField]
    Vector3 camOffsetPos, camOffsetRotate;

    [SerializeField]
    GameObject player;

    [SerializeField]
    float playerSpeed, sideSpeed;

    [SerializeField]
    Animator playerAnimator;

    [SerializeField]
    InputActionReference inputAction;
    #endregion

    new Camera camera;
    Rigidbody playerBody;


    private void Awake() {
        PrepareScene();//hardware preparation
    }

    private void Start() {
        LocalStore.player = player;//cashing player
        LocalStore.playerAnimator = playerAnimator;//cashing animator

        playerBody = LocalStore.player.GetComponent<Rigidbody>();
        LocalStore.gameState = StateMachine.GameState.ready;//first state
    }

    private void FixedUpdate() {
        if(LocalStore.gameState == StateMachine.GameState.ready && inputAction.action.WasPressedThisFrame()){
            LocalStore.gameState = StateMachine.GameState.game;//game state
        }
        camera.gameObject.transform.position = LocalStore.cameraPlace(LocalStore.player.transform, camOffsetPos);//refresh camera pos
        StateMachine.PlayerBehaviour(playerBody, LocalStore.gameState, playerSpeed, sideSpeed, inputAction);//player beh
        StateMachine.AnimationSense(LocalStore.playerAnimator, playerBody, LocalStore.gameState);// anim state
    }

    void PrepareScene(){
        QualitySettings.vSyncCount= 0;//enabled tfr
        Application.targetFrameRate = 60;//up
        camera = Camera.main;//putty camera
        camera.gameObject.transform.rotation = Quaternion.Euler(camOffsetRotate);//fix rotation
    }
}
