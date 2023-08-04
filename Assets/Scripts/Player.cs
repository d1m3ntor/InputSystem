using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, PlayerActions.IPlayer_MapActions
{
    private PlayerActions playerActions;
    private float movementSpeed = 3;
    private Vector2 direction;
    private Vector3 cursorPosition;
    
    private void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.Player_Map.SetCallbacks(this);
    }
    
    private void OnEnable() => playerActions.Player_Map.Enable();
    private void OnDisable() => playerActions.Player_Map.Disable();

    private void Update()
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime);
        LookAtCursor();
    }

    private void LookAtCursor()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
        var lookDirection = (mouseScreenPosition - (Vector2)transform.position).normalized;
        transform.up = lookDirection;
    }

    public void OnMovement(InputAction.CallbackContext context) => direction = context.action.ReadValue<Vector2>();
    public void OnPointerPosition(InputAction.CallbackContext context) => cursorPosition = context.action.ReadValue<Vector2>();
}