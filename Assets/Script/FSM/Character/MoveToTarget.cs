using Script.Scene.Game.Character.CharacterBase;
using UnityEngine;

namespace Script.FSM.Character
{
    public class MoveToTarget : StateBase
    {
        private PersonBase myPerson;

        public MoveToTarget(PersonBase mySelf, FsmSystem fsmSystem, Animator _anim)
        {
            myPerson = mySelf;
            stateID = StateID.MoveToTarget;
            Fsm = fsmSystem;
            anim = _anim;
        }

        public override void CheckTransition(PersonBase targetPerson)
        {
            if (targetPerson == null)
            {
                //没有目标则寻找目标
                Fsm.PerformTransition(Transition.SeekTarget);
                return;
            }

            if (!targetPerson.personData.Alive)
            {
                Fsm.PerformTransition(Transition.SeekTarget);
                return;
                //如果目标死亡则寻敌
            }

            Vector3 targetPos = targetPerson.transform.position;
            targetPos.y = myPerson.transform.position.y;
            float distance = Vector3.Distance(targetPos, myPerson.transform.position);
            if (myPerson.personData.AttackDistance >= distance)
            {
                //转到攻击状态
                Fsm.PerformTransition(Transition.Attack);
            }

            Debug.Log("处于移动状态");
        }

        public override PersonBase Act(PersonBase targetPerson)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                anim.Play("Run");
            }
            if (targetPerson && targetPerson.personData.Alive)
            {
                Vector3 targetPos = targetPerson.transform.position;
                targetPos.y = myPerson.transform.position.y;
                Vector3 moveDir = targetPos - myPerson.transform.position;
                //移动到目标位置
                myPerson.transform.position += moveDir.normalized * myPerson.personData.MoveSpeed * Time.deltaTime;
                //转向
                myPerson.transform.rotation = Quaternion.Slerp(myPerson.transform.rotation,
                    Quaternion.LookRotation(moveDir), 5 * Time.deltaTime);
                myPerson.transform.eulerAngles = new Vector3(0, myPerson.transform.eulerAngles.y, 0);
            }

            return targetPerson;
        }
    }
}