using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed;
	public float MoveSpeed { get { return moveSpeed; } }

	[SerializeField]
	private float detectRange; //��������
	public float DetectRange { get { return detectRange; } }

	[SerializeField]
	private float attackRange; //���� ����
	public float AttackRange { get { return attackRange; } }

	[SerializeField]
	private float attackTime; //���� �ð� (��Ÿ��?)
	public float AttackTime { get { return attackTime; } }

	[SerializeField]
	private float attackPower; //���� �� (damage)
	public float AttackPower { get { return attackPower; } }

	[SerializeField]
	private float maxHP;
	public float MaxHP { get { return maxHP; } }
}
