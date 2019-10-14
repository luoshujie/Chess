using UnityEngine;

namespace Script.FSM
{
    public class FollowPathState : StateBase
    {

        private int currentWayPoint;
        private Transform[] wayPoint;

        public FollowPathState(Transform[] wp)
        {
            wayPoint = wp;
            currentWayPoint = 0;
            stateID = StateID.MoveToTarget;
        }
        public override void CheckTransition(GameObject self, GameObject target)
        {
            //本函数一旦被调用，将会返回以参数1为原点和参数2为半径的球体内“满足一定条件”的碰撞体集合
            Collider[] colliders = Physics.OverlapSphere(target.transform.position, 5f);
            if (colliders.Length <= 0) return;
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.CompareTag("Player"))
                {
                    target.GetComponent<NpcContorl>().SetTransition(Transition.Attack);
                }
            }
        }

        public override void Act(GameObject self, GameObject target)
        {
            Rigidbody rb = target.GetComponent<Rigidbody>();
            Vector3 vel = rb.velocity;
            Vector3 moveDir = wayPoint[currentWayPoint].position-target.transform.position;
            if (moveDir.magnitude<1)
            {
                currentWayPoint++;
                if (currentWayPoint>=wayPoint.Length)
                {
                    currentWayPoint = 0;
                }
            }
            else
            {
                vel = moveDir.normalized * 10;
                target.transform.rotation=Quaternion.Slerp(target.transform.rotation,Quaternion.LookRotation(moveDir),5*Time.deltaTime );
                target.transform.eulerAngles=new Vector3(0,target.transform.eulerAngles.y,0);
            }

            rb.velocity = vel;
        }
    }
}
