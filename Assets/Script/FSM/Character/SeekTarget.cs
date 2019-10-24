using Script.Scene.Game.Character.CharacterBase;
using Script.Scene.Game.Manager;
using UnityEngine;

namespace Script.FSM.Character
{
    public class SeekTarget : StateBase
    {
        private PersonBase myPerson;

        public SeekTarget(PersonBase myself, FsmSystem fsmSystem, Animator _anim)
        {
            myPerson = myself;
            stateID = StateID.SeekTarget;
            Fsm = fsmSystem;
            anim = _anim;
        }

        public override void CheckTransition(PersonBase targetPerson)
        {
            if (targetPerson && targetPerson.personData.Alive)
            {
                Vector3 targetPos = targetPerson.transform.position;
                targetPos.y = myPerson.transform.position.y;
                float distance = Vector3.Distance(targetPos, myPerson.transform.position);
                if (myPerson.personData.AttackDistance >= distance)
                {
                    //转到攻击状态
                    Fsm.PerformTransition(Transition.Attack);
                    return;
                }

                //移动到目标位置状态
                Fsm.PerformTransition(Transition.MoveToTarget);
            }

            Debug.Log("处于寻敌状态");
        }

        public override PersonBase Act(PersonBase targetPerson)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                anim.Play("Idle");
            }
            //查找目标
//            targetPerson = FightMgr.instance.GetEnemyAtMinDistance(myPerson, out distance);

            GameObject targetModel = GameObject.FindWithTag("Enemy");
            targetPerson = targetModel?.GetComponent<PersonBase>();
            return targetPerson;
        }
    }
}