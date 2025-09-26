using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string moveParameterName = "Move";
    [SerializeField] private string dirParameterName = "dir";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string groundParameterName = "Ground";
    [SerializeField] private string airParameterName = "Air";
    [SerializeField] private string fallParameterName = "isfall";


    public int moveParameterHash { get; private set; }
    public int dirParameterHash { get; private set; }
    public int jumpParameterHash { get; private set; }
    public int groundParameterHash { get; private set; }
    public int airParameterHash { get; private set; }
    public int fallParameterHash { get; private set; }

    public void Init()
    {
        moveParameterHash = Animator.StringToHash(moveParameterName);
        dirParameterHash = Animator.StringToHash(jumpParameterName);
        jumpParameterHash = Animator.StringToHash(jumpParameterName);
        groundParameterHash = Animator.StringToHash(jumpParameterName);
        airParameterHash = Animator.StringToHash(jumpParameterName);
        fallParameterHash = Animator.StringToHash(fallParameterName);
    }
}
