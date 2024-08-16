
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class HotkeyBinding : MonoBehaviour
{
    public InputActionAsset inputActions;
    public Dictionary<string, InputAction> actionBindings;

    //Using the observer pattern for potential future additions
    private List<IColorObserver> colorObservers = new List<IColorObserver>();
    private List<IMovementObserver> movementObservers = new List<IMovementObserver>();
    private void Awake() {
        //Add hotkey binds with input actions so players can bind hotkeys through the menu settings in the future (when menu becomes available)
        actionBindings = new Dictionary<string, InputAction>();
        InputActionMap gameplayActions = inputActions.FindActionMap("GamePlay");

        actionBindings["Action1"] = gameplayActions.FindAction("Action1");
        actionBindings["Action2"] = gameplayActions.FindAction("Action2");
        actionBindings["Action3"] = gameplayActions.FindAction("Action3");
        
        actionBindings["MoveLeft"] = gameplayActions.FindAction("MoveLeft");
        actionBindings["MoveRight"] = gameplayActions.FindAction("MoveRight");

        actionBindings["Jump"] = gameplayActions.FindAction("Jump");

        actionBindings["Action1"].performed += ctx => NotifyColorObservers(Color.red);
        actionBindings["Action2"].performed += ctx => NotifyColorObservers(Color.green);
        actionBindings["Action3"].performed += ctx => NotifyColorObservers(Color.blue);

        actionBindings["MoveLeft"].performed += ctx => NotifyMovementObservers(MovementXConverter());
        actionBindings["MoveRight"].performed += ctx => NotifyMovementObservers(MovementXConverter());
        actionBindings["Jump"].performed += ctx => NotifyJumpObservers();

        
    }

    void Update(){

        if(actionBindings["MoveLeft"].IsPressed()){
            NotifyMovementObservers(MovementXConverter());
        }
        if(actionBindings["MoveRight"].IsPressed()){
            NotifyMovementObservers(MovementXConverter());
        }
    }

    // Update is called once per frame
    private void OnEnable()
    {
        foreach (var action in actionBindings.Values)
        {
            action.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var action in actionBindings.Values)
        {
            action.Disable();
        }
    }
    #region Observer/Listener Pattern
    public void RegisterColorObserver(IColorObserver observer)
    {
        if (!colorObservers.Contains(observer))
        {
            colorObservers.Add(observer);
        }
    }

    public void UnregisterColorObserver(IColorObserver observer)
    {
        if (colorObservers.Contains(observer))
        {
            colorObservers.Remove(observer);
        }
    }

    private void NotifyColorObservers(Color color)
    {
        foreach (IColorObserver observer in colorObservers)
        {
            observer.OnColorChange(color);
        }
    }

    public void RegisterMovementObserver(IMovementObserver observer)
    {
        if (!movementObservers.Contains(observer))
        {
            movementObservers.Add(observer);
        }
    }

    public void UnregisterMovementObserver(IMovementObserver observer)
    {
        movementObservers.Remove(observer);
    }

    private void NotifyMovementObservers(Vector2 direction)
    {
        foreach (IMovementObserver observer in movementObservers)
        {
            observer.OnMove(direction);
        }
    }
    private void NotifyJumpObservers()
    {
        foreach (var observer in movementObservers)
        {
            observer.OnJump();
        }
    }

    #endregion

    #region Helper Functions
    
    private Vector2 MovementXConverter(){
        float moveX = 0f;
        if(actionBindings["MoveLeft"].ReadValue<float>() > 0){
            moveX -= 1;
        }
        if(actionBindings["MoveRight"].ReadValue<float>() > 0){
            moveX += 1;
        }

        Vector2 direction = new Vector2(moveX, 0);
        return direction;
    }

    #endregion
}
