using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleAttackState : PlayerBaseState
{
    #region PRIVATE_FIELDS
    private string PlayerMeleAnimationName = "Player_Mele_HIt";
    private Vector3 sphereCenter;
    #endregion
    
    public PlayerMeleAttackState(string name, State_Machine stateMachine, PlayerComponent player) : base(name, stateMachine, player)
    {
    }
    
    public override void OnEnter()
    {
        sphereCenter = _player.transform.position + _player.transform.right * _player._attackRange;
        _player.input.OnPlayerAttack += OnMeleeAttack;
        MeleeAttack();
        base.OnEnter();
    }

    private void OnMeleeAttack(bool obj)
    {
        MeleeAttack();
    }

    public override void UpdateLogic()
    {
        sphereCenter = _player.transform.position + _player.transform.right * _player._attackRange;
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    private void playMeleAttackAnimation()
    {
        _player.anim.Play(PlayerMeleAnimationName);
    }
    
    private void MeleeAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(sphereCenter,_player._attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyInputManager meleeEnemy = enemy.GetComponent<EnemyInputManager>();
            DistanceEnemy distanceEnemy = enemy.GetComponent<DistanceEnemy>();

            if (meleeEnemy != null)
                meleeEnemy.OnTakeDamage();
            else if (distanceEnemy != null)
                distanceEnemy.TakeDamage(_player.damage);
        }

        playMeleAttackAnimation();
    }

    public override void AddStateTransitions(string transitionName, State transitionState)
    {
        base.AddStateTransitions(transitionName, transitionState);
    }

    public override void OnExit()
    {
        _player.input.OnPlayerAttack -= OnMeleeAttack;
        _player.anim.StopPlayback();
        base.OnExit();
    }
}
