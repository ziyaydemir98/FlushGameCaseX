
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    Animator _animator;
    public override void EnterState(PlayerManager player)
    {
        _animator=player.GetComponent<Animator>();
        _animator.SetBool("IsMove", false);
    }
}
