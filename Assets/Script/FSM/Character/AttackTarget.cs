using Script.Scene.Game.Character.CharacterBase;
using Script.Scene.Game.Manager;
using UnityEngine;

namespace Script.FSM.Character
{
    public class AttackTarget : StateBase
    {
        public PersonBase myPerson;

        public AttackTarget(PersonBase myself, FsmSystem fsmSystem, Animator _anim)
        {
            myPerson = myself;
            stateID = StateID.Attack;
            Fsm = fsmSystem;
            anim = _anim;
        }

        public override void CheckTransition(PersonBase targetPerson)
        {
            if (targetPerson == null)
            {
                Fsm.PerformTransition(Transition.SeekTarget);
                return;
            }

            if (!targetPerson.personData.Alive)
            {
                Fsm.PerformTransition(Transition.SeekTarget);
                return;
            }

            Vector3 targetPos = targetPerson.transform.position;
            targetPos.y = myPerson.transform.position.y;
            float distance = Vector3.Distance(targetPos, myPerson.transform.position);
            if (myPerson.personData.AttackDistance < distance)
            {
                //转到攻击状态
                Fsm.PerformTransition(Transition.MoveToTarget);
            }

            Debug.Log("处于攻击状态");
        }

        public override PersonBase Act(PersonBase targetPerson)
        {
            if (myPerson.personData.Alive && myPerson.CurrAttackTime <= 0)
            {
                if (targetPerson && targetPerson.personData.Alive)
                {
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        anim.Play("Attack");
                    }
                    //攻击
                    FightMgr.instance.Hurt(myPerson, targetPerson);
                    myPerson.CurrAttackTime = myPerson.personData.AttackSpeed;
                    Debug.Log("Attack");
                }
            }

            return targetPerson;
        }
    }
}