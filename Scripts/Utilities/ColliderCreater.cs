using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(EdgeCollider2D))]
public class ColliderCreater : MonoBehaviour
{
	[SerializeField] private Transform pointsHolder;

	private Vector2? firstPoint;

	protected void Start()
	{
		List<Vector2> positions = new List<Vector2>();
		firstPoint = null;

		foreach (Transform point in pointsHolder)
		{
			if (point != transform)
			{
				if (firstPoint == null)
				{
					firstPoint = point.position;
				}

				positions.Add(point.position);
			}
		}

		positions.Add((Vector2)firstPoint);

		GetComponent<EdgeCollider2D>().points = positions.ToArray();
	}
}