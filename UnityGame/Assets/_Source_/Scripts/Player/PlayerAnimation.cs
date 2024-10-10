using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ToWalk(bool isWalk)
    {
        _animator.SetBool(AnimationData.Character.IsWalk, isWalk);
    }

    public void ToBonus()
    {
        _animator.SetTrigger(AnimationData.Character.Bonus);
    }
}
