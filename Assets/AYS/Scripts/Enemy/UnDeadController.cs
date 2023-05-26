using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnDead;
using UnityEditor.Experimental.GraphView;
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
	}

	protected override void Start()
	{
		base.Start();

		stateMachine.SetUp(EnemyStateType.Idle);
	}

	private void Update()
	{
		if(curHP > 0)
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
		Animator.SetBool("IsAttack", false);

		Animator.SetTrigger("DoDie");
		Destroy(20f);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, DataModel.DetectRange);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, DataModel.AttackRange);
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

	//todo. �¿�θ� ���󰡵��� ����
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
			animator.SetBool("IsAttack", true);
			lastAttakTime = 0;
		}

		public override void Exit()
		{
			animator.SetBool("IsAttack", false);
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
				animator.SetTrigger("DoAttack");

				//todo. ���� ���� 
				Debug.Log($"[����] {owner.DataModel.AttackPower}");

				//owner.GetDamange(3f);

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
}
