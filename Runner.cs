using Unity.VisualScripting;
using UnityEngine;

public class Runner : BaseEnemy
{

    [SerializeField] private string ANIM_DAMAGE_TRIGGER = "takeDamage";
    [SerializeField]private Animator animator;

    protected override void CustomAwake()
    {
        OnHealthChange += Runner_OnHealthChange;
    }

    private void Runner_OnHealthChange(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ANIM_DAMAGE_TRIGGER);
    }

    public override void Attack()
    {
        if (Player.Instance.GetInShadows()) return;
        Player.Instance.ChangeHealth(-attackDamage);
    }
}