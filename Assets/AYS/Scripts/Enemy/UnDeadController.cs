using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnDead;
using UnityEngine.Events;

public class UnDeadController : Enemy
{
	private StateMachine<EnemyStateType, UnDeadController> stateMachine;

	protected override void Awake()
	{
		base.Awake();

		stateMachine = new StateMachine<EnemyStateType, UnDeadController>(this);
		stateMachine.AddState(EnemyStateType.Idle, new IdleState(this, stateMachine));
		stateMachine.AddState(EnemyStateType.Trace, new TraceState(this, stateMachine));
		stateMachine.AddState(EnemyStateType.Attack, new AttackState(this, stateMachine));
		stateMachine.AddState(EnemyStateType.Return, new ReturnState(this, stateMachine));
		stateMachine.AddState(EnemyStateType.Die, new DieState(this, stateMachine));
	}

	protected override void Start()
	{
		base.Start();

		ReutnrPosition = transform.position;

		stateMachine.SetUp(EnemyStateType.Idle);
	}

	private void Update()
	{
		stateMachine.Update();
	}

	protected void Die()
	{
		stateMachine.ChangeState(EnemyStateType.Die);
	}
}


namespace UnDead
{
	public class IdleState : EnemyStatePattern<UnDeadController>
	{
		public IdleState(UnDeadController owner, StateMachine<EnemyStateType, UnDeadController> stateMachine)
			: base(owner, stateMachine)
		{
		}

		public override void Setup()
		{
			target = owner.Target;
		}

		public override void Enter()
		{
			rigidbody.velocity = Vector2.zero;
		}

		public override void Exit()
		{
		}

		public override void Transition()
		{
			//�÷��̾ ���� ������ ������?
			if (Vector2.Distance(target.position, transform.position) < owner.DataModel.DetectRange)
			{
				stateMachine.ChangeState(EnemyStateType.Trace); //���� ���·� ����
			}
		}

		public override void Update()
		{
		}
	}

	public class TraceState : EnemyStatePattern<UnDeadController>
	{
		public TraceState(UnDeadController owner, StateMachine<EnemyStateType, UnDeadController> stateMachine)
			: base(owner, stateMachine)
		{
		}

		public override void Setup()
		{
			target = owner.Target;
		}

		public override void Enter()
		{
			animator.SetBool("IsMove", true);
		}

		public override void Exit()
		{
			animator.SetBool("IsMove", false);
		}

		public override void Transition()
		{
			//�÷��̾ ���� ������ ������?
			if (Vector2.Distance(target.position, transform.position) < owner.DataModel.AttackRange)
			{
				stateMachine.ChangeState(EnemyStateType.Attack); //���� ���·� ����
			}

			//�÷��̾ ���� ������ �����?
			else if (Vector2.Distance(target.position, transform.position) > owner.DataModel.DetectRange)
			{
				stateMachine.ChangeState(EnemyStateType.Return); //���ڸ��� ���� ����
			}
		}

		public override void Update()
		{
			//�÷��̾� �Ѿư���
			renderer.flipX = (target.position.x < transform.position.x);

			var dir = (target.position - transform.position).normalized;
			transform.Translate(dir * owner.DataModel.MoveSpeed * Time.deltaTime);
		}
	}

	public class AttackState : EnemyStatePattern<UnDeadController>
	{
		private float lastAttakTime;

		public AttackState(UnDeadController owner, StateMachine<EnemyStateType, UnDeadController> stateMachine)
			: base(owner, stateMachine)
		{
		}

		public override void Setup()
		{
			target = owner.Target;
		}

		public override void Enter()
		{
			lastAttakTime = 0;
		}

		public override void Exit()
		{
		}

		public override void Transition()
		{
			//�÷��̾ ���� ������ �����?
			if (Vector2.Distance(target.position, transform.position) > owner.DataModel.AttackRange)
			{
				stateMachine.ChangeState(EnemyStateType.Trace); //���� ���·� ����
			}
		}

		public override void Update()
		{
			if (lastAttakTime > owner.DataModel.AttackTime)
			{
				//todo. ���� ���� 
				UnityEngine.Debug.Log("����");
				lastAttakTime = 0;
			}
			lastAttakTime += Time.deltaTime;
		}
	}

	public class ReturnState : EnemyStatePattern<UnDeadController>
	{
		public ReturnState(UnDeadController owner, StateMachine<EnemyStateType, UnDeadController> stateMachine)
			: base(owner, stateMachine)
		{
		}

		public override void Setup()
		{
			target = owner.Target;
		}

		public override void Enter()
		{
			animator.SetBool("IsMove", true);
		}

		public override void Exit()
		{
			animator.SetBool("IsMove", false);
		}

		public override void Transition()
		{
			//���ڸ��� ���ƿ���? 
			if (Vector2.Distance(transform.position, owner.ReutnrPosition) < 0.02f)
			{
				Debug.Log("[ReturnState] ���ڸ���!!");
				stateMachine.ChangeState(EnemyStateType.Idle); //��� ���·� ����
			}

			//�÷��̾ ���� ������ ������?
			else if (Vector2.Distance(target.position, transform.position) < owner.DataModel.DetectRange)
			{
				stateMachine.ChangeState(EnemyStateType.Trace); //���� ���·� ����
			}
		}

		public override void Update()
		{
			renderer.flipX = (owner.ReutnrPosition.x < transform.position.x);

			Vector2 dir = (owner.ReutnrPosition - transform.position).normalized; //������ �ӵ��� ���ڸ��� ���ư����� ��
			transform.Translate(dir * owner.DataModel.MoveSpeed * Time.deltaTime);
		}
	}

	public class DieState : EnemyStatePattern<UnDeadController>
	{
		public DieState(UnDeadController owner, StateMachine<EnemyStateType, UnDeadController> stateMachine)
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
			
		}

		public override void Update()
		{
		}
	}
}
