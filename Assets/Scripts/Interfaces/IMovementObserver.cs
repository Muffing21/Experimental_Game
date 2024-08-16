using Vector2 = UnityEngine.Vector2;

public interface IMovementObserver{
    void OnMove(Vector2 direction);
    void OnJump();
}