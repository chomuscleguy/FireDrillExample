using UnityEngine;

public interface IState
{
    public void Enter();
    public void HandleInput();
    public void Update();
    public void FixedUpdate();
    public void Exit();
}
