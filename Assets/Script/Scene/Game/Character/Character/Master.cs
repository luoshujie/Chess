using Script.Enum;
using Script.FSM;
using Script.FSM.Character;
using Script.Scene.Game.Character.CharacterBase;
using Script.Scene.Game.Character.CharacterData;
using UnityEngine;

namespace Script.Scene.Game.Character.Character
{
    public class Master : PersonBase
    {
        private PersonBase targetPerson;

        private FsmSystem fsm;

        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            personData = new PersonData(CharacterTypeEnum.Enemy, 1, 1, 100, 1, 1, 1, 2, 10, 0, 2);
            Init(personData);
            InitFSMSystem();
        }

        //设置状态机状态
        public void SetTransition(Transition trans)
        {
            fsm.PerformTransition(trans);
        }

        private void FixedUpdate()
        {
            fsm.CurrentState.CheckTransition(targetPerson);
            targetPerson = fsm.CurrentState.Act(targetPerson);
        }

        private void InitFSMSystem()
        {
            fsm = new FsmSystem();

            SeekTarget seekTarget = new SeekTarget(this, fsm, anim);
            seekTarget.AddTransition(Transition.MoveToTarget, StateID.MoveToTarget);
            seekTarget.AddTransition(Transition.Attack, StateID.Attack);

            MoveToTarget moveToTarget = new MoveToTarget(this, fsm, anim);
            moveToTarget.AddTransition(Transition.SeekTarget, StateID.SeekTarget);
            moveToTarget.AddTransition(Transition.Attack, StateID.Attack);

            AttackTarget attackTarget = new AttackTarget(this, fsm, anim);
            attackTarget.AddTransition(Transition.SeekTarget, StateID.SeekTarget);
            attackTarget.AddTransition(Transition.MoveToTarget, StateID.MoveToTarget);

            fsm.AddState(seekTarget);
            fsm.AddState(moveToTarget);
            fsm.AddState(attackTarget);
        }
    }
}