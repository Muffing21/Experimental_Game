using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotkeyBinding : MonoBehaviour
{
    public InputActionAsset inputActions;
    private Dictionary<string, InputAction> actionBindings;
    private void Awake() {

        actionBindings = new Dictionary<string, InputAction>();
        InputActionMap gameplayActions = inputActions.FindActionMap("GamePlay");

        actionBindings["Action1"] = gameplayActions.FindAction("Action1");
        actionBindings["Action2"] = gameplayActions.FindAction("Action2");
        actionBindings["Action3"] = gameplayActions.FindAction("Action3");
        

        actionBindings["Action1"].performed += ctx => Action1();
        actionBindings["Action2"].performed += ctx => Action2();
        actionBindings["Action3"].performed += ctx => Action3();
        
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

    private void Action1(){
        GameManager.Instance.SetPlayerColor(Color.red);
    }
    private void Action2() {
        GameManager.Instance.SetPlayerColor(Color.green);
    }
    private void Action3() {
        GameManager.Instance.SetPlayerColor(Color.blue);
    }
    
}
