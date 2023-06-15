
using UnityEngine;

public class PlayerRunState : PlayerState
{
    Animator _animator;
    public override void EnterState(PlayerManager player)
    {
        _animator = player.GetComponent<Animator>();
        _animator.SetBool("IsMove", true);
    }
}
