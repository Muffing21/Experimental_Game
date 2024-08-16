using UnityEngine;

public class PlayerColorChanger : MonoBehaviour, IColorObserver
{
    public HotkeyBinding hotkeyBind;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnEnable() {
        hotkeyBind.RegisterColorObserver(this);
    }

    private void OnDisable() {
        hotkeyBind.UnregisterColorObserver(this);
    }
    public void OnColorChange(Color color){
        GameManager.Instance.SetPlayerColor(color);    
    }
}
