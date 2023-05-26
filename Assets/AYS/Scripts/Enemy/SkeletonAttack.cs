using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
	[SerializeField]
	private float AttackSpeed; //���� �ӵ�

	Transform target => GameObject.FindGameObjectWithTag("Player").transform;
	Vector3 targetPos;

	void Start()
	{
		targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
	}

	private void Update()
	{
		DoAttack();
		ClearAttack();
	}

	private void DoAttack()
	{
		var dir = (targetPos - transform.position).normalized;
		transform.Translate(dir * AttackSpeed * Time.deltaTime);
	}

	private void ClearAttack()
	{
		//��ǥ������ �����ϸ�?
		if (Vector2.Distance(transform.position, targetPos) < 0.02f)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == target.name)
		{
			//todo.�÷��̾� ������ ����
			Debug.Log("����!");
			Destroy(gameObject);
		}
	}
}
