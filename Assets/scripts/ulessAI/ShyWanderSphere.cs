using UnityEngine;
using System.Collections;

public class ShyWanderSphere : MonoBehaviour {
	public float CircleRadius = 1;
	public float TurnChance = 0.05f;
	public float MaxRadius = 5;

	public float Mass = 15;
	public float MaxSpeed = 3;
	public float MaxForce = 15;

	private Vector3 velocity;
	private Vector3 wanderForce;
	private Vector3 target;

	public string searchTag;
	public GameObject closetMissle;
	public float safeDistance;

	private void Start()
	{
		velocity = Random.onUnitSphere;
		wanderForce = GetRandomWanderForce();
	}

	private void Update()
	{
		closetMissle = FindClosestEnemy ();
		float distance = Vector3.Distance (transform.position, closetMissle.transform.position);

		if (distance < safeDistance)
		{
			transform.Translate (Vector3.back * 2.0f * Time.deltaTime);
		}

		var desiredVelocity = GetWanderForce();
		desiredVelocity = desiredVelocity.normalized * MaxSpeed;

		var steeringForce = desiredVelocity - velocity;
		steeringForce = Vector3.ClampMagnitude(steeringForce, MaxForce);
		steeringForce /= Mass;

		velocity = Vector3.ClampMagnitude(velocity + steeringForce, MaxSpeed);
		transform.position += velocity * Time.deltaTime;
		transform.forward = velocity.normalized;

		Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
		Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);
	}

	GameObject FindClosestEnemy()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag(searchTag);

		GameObject closest = null;
		float distance = Mathf.Infinity;

		Vector3 position = transform.position;

		foreach(GameObject go in gos)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if(curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}

		return closest;
	}

	private Vector3 GetWanderForce()
	{
		if (transform.position.magnitude > MaxRadius)
		{
			var directionToCenter = (target - transform.position).normalized;
			wanderForce = velocity.normalized + directionToCenter;
		}
		else if (Random.value < TurnChance)
		{
			wanderForce = GetRandomWanderForce();
		}

		return wanderForce;
	}

	private Vector3 GetRandomWanderForce()
	{
		var circleCenter = velocity.normalized;
		var randomPoint = Random.insideUnitCircle;

		var displacement = new Vector3(randomPoint.x, randomPoint.y) * CircleRadius;
		displacement = Quaternion.LookRotation(velocity) * displacement;

		var wanderForce = circleCenter + displacement;
		return wanderForce;
	}
}
