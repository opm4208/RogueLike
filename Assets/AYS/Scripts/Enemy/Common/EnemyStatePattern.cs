using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStatePattern<T> : EnemyState<EnemyStateType, T> where T : Enemy
{
	/* ���ٽ����� ������ ������? 
	*	������ �����ϸ�, �ѹ� ������ �Ҵ����ְ� ��������, 
	*	���ٽ����� ������ ���ָ� �ش� ������ ȣ�� �Ǵ� ������ ���� �����͸� �Ź�Ȯ���ϴ� ���°� ��.
	*	
	*	���� ��� 
	*	Rigidbody2D rigidbody = owner.rigidbody;�� �ѹ� �Ҵ����ָ� ����.
	*	����, �Ҵ� ������ owner�� rigidbody�� ������ �ٽ� �Ҵ���� ������ rigidbody�� null��,
	*	������, ���ٽ��� Ȱ���� Rigidbody2D rigidbody => owner.rigidbody; �Ҵ����ָ�
	*	rigidbody�� ����Ϸ��� ȣ�� �� ������ owner�� rigidbody�� ��������. 
	*	����, ó������ owner�� ���� ���� null�� �Ҵ� �Ǿ�����, ���߿� rigidbody�� �߰��Ǹ�
	*	�߰��� �Ŀ� rigidbody�� ȣ��� owner�� rigidbody�� �������� ��. 
	*/

	protected GameObject gameObject => owner.gameObject;
	protected Transform transform => owner.transform;
	protected Rigidbody2D rigidbody => owner.Rigidbody;
	protected SpriteRenderer renderer => owner.SpriteRnder;
	protected Animator animator => owner.Animator;
	protected Collider2D collider => owner.Collider;

	/// <summary>
	/// player transform
	/// </summary>
	protected Transform target;

	protected EnemyStatePattern(T owner, StateMachine<EnemyStateType, T> stateMachine)
		: base(owner, stateMachine)
	{
	}
}