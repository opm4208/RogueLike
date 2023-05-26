using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
	private Rigidbody2D rigidbd;
	public Rigidbody2D Rigidbody { get { return rigidbd; } }

	private Animator animator;
	public Animator Animator { get { return animator; } }

	private Collider2D colider;
	public Collider2D Collider { get { return colider; } }

	private SpriteRenderer spriteRnder;
	public SpriteRenderer SpriteRnder { get { return spriteRnder; } }

	[SerializeField]
	private EnemyModel dataModel;
	public EnemyModel DataModel { get { return dataModel; } }

	/// <summary>
	/// ���� ü��
	/// </summary>
	public float curHP { get; private set; }

	/// <summary>
	/// �÷��̾�
	/// </summary>
	public Transform Target { get { return target; } }
	private Transform target;

	/// <summary>
	/// ���� ��ġ
	/// </summary>
	public Vector3 ReutnrPosition { get; protected set; }

	protected virtual void Awake()
	{
		rigidbd = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		colider = GetComponent<Collider2D>();
		spriteRnder = GetComponent<SpriteRenderer>();

		curHP = dataModel.MaxHP;
	}

	protected virtual void Start()
	{
		ReutnrPosition = transform.position;
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public virtual void GetDamange(float damage)
	{
		curHP -= damage;
	}

	public void Destroy(float time = 0)
	{
		Destroy(gameObject, time);
	}
}
