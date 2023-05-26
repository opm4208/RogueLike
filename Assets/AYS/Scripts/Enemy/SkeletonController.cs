using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Path;
using UnityEngine;
using Skeleton;
using Unity.VisualScripting;

public class SkeletonController : Enemy
{
	[Header("Shooter")]
	[SerializeField]
	private GameObject AttackObj;

	[SerializeField]
	private Transform AttackPoint;

	private StateMachine<EnemyStateType, SkeletonController> stateMachine;

	protected override void Awake()
	{
		base.Awake();

		stateMachine = new StateMachine<EnemyStateType, SkeletonController>(this);
		stateMachine.AddState(EnemyStateType.Idle, new IdleState(this, stateMachine));
		stateMachine.AddState(EnemyStateType.Attack, new AttackState(this, stateMachine));
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
	}

	public override void GetDamange(float damage)
	{
		base.GetDamange(damage);

		if (curHP <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		Animator.SetTrigger("DoDie");
		Destroy(20f);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, DataModel.AttackRange);
	}

	public void MakeAttack()
	{
		Instantiate(AttackObj, AttackPoint.position, AttackPoint.rotation);
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
				owner.MakeAttack();

				lastAttakTime = 0;

				//owner.GetDamange(owner.DataModel.AttackPower);
			}
			lastAttakTime += Time.deltaTime;

		}
	}
}
