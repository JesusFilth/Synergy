using UnityEngine;

public class AnimationData
{
    public class Character
    {
        public static int IsWalk = Animator.StringToHash(nameof(IsWalk));
        public static int Idel = Animator.StringToHash(nameof(Idel));
        public static int Bonus = Animator.StringToHash(nameof(Bonus));
    }
}