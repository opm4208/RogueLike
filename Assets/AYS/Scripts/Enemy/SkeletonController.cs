using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Path;
using UnityEngine;
using Skeleton;
using Unity.VisualScripting;

public class SkeletonController : Enemy
{
	private StateMachine<EnemyStateType, SkeletonController> stateMachine;

	protected override void Awake()
	{
		base.Awake();

		stateMachine = new StateMachine<EnemyStateType, SkeletonController>(this);
		stateMachine.AddState(EnemyStateType.Idle, new IdleState(this, stateMachine));
		stateMachine.AddState(EnemyStateType.Attack, new AttackState(this, stateMachine));
		stateMachine.AddState(EnemyStateType.Die, new DieState(this, stateMachine));
	}

	protected override void Start()
	{
		base.Start();

		stateMachine.SetUp(EnemyStateType.Idle);
	}

	private void Update()
	{
		if (curHP > 0)
		{
			stateMachine.Update();
		}
		else
		{
			Die();
		}
	}

	protected void Die()
	{
		stateMachine.ChangeState(EnemyStateType.Die);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, DataModel.AttackRange);
	}
}


namespace Skeleton
{
	public class IdleState : EnemyStatePattern<SkeletonController>
	{
		public IdleState(SkeletonController owner, StateMachine<EnemyStateType, SkeletonController> stateMachine)
			: base(owner, stateMachine)
		{
		}

		public override void Setup()
		{
			target = owner.Target;
		}

		public override void Enter()
		{
		}

		public override void Exit()
		{
		}

		public override void Transition()
		{
			//�÷��̾ ���� ������ ������
			if (Vector2.Distance(target.position, transform.position) < owner.DataModel.AttackRange)
			{
				stateMachine.ChangeState(EnemyStateType.Attack); //���� ���·� ����
			}
		}

		public override void Update()
		{
		}
	}

	public class AttackState : EnemyStatePattern<SkeletonController>
	{
		private float lastAttakTime;

		public AttackState(SkeletonController owner, StateMachine<EnemyStateType, SkeletonController> stateMachine)
			: base(owner, stateMachine)
		{
		}

		public override void Setup()
		{
			target = owner.Target;
		}

		public override void Enter()
		{
			lastAttakTime = owner.DataModel.AttackTime;
		}

		public override void Exit()
		{
		}

		public override void Transition()
		{
			//�÷��̾ ���� ������ �����?
			if (Vector2.Distance(target.position, transform.position) > owner.DataModel.AttackRange)
			{
				stateMachine.ChangeState(EnemyStateType.Idle); //���� ���·� ����
			}
		}

		public override void Update()
		{
			renderer.flipX = (target.position.x < transform.position.x);

			if (lastAttakTime > owner.DataModel.AttackTime)
			{
				animator.SetTrigger("DoAttack");

				//todo. ���� ���� 
				Debug.Log($"[����] {owner.DataModel.AttackPower}");

				lastAttakTime = 0;
			}
			lastAttakTime += Time.deltaTime;

		}
	}

	public class DieState : EnemyStatePattern<SkeletonController>
	{
		public DieState(SkeletonController owner, StateMachine<EnemyStateType, SkeletonController> stateMachine)
			: base(owner, stateMachine)
		{
		}

		public override void Setup()
		{
			target = owner.Target;
		}

		public override void Enter()
		{
			owner.Destroy();
		}

		public override void Exit()
		{
		}

		public override void Transition()
		{

		}

		public override void Update()
		{
		}
	}
}
