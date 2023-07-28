using UnityEngine;

namespace RandomPlacer.PositionProviders
{
	public class CirclePositionProvider : PositionProvider
	{
		[field: SerializeField] public float Radius { get; set; } = 1f;

		[field: SerializeField] public Vector3 Center { get; set; } = Vector3.zero;

		[field: SerializeField] public Axis NormalAxis { get; set; } = Axis.X;

		public override Vector3 GetPosition() =>
				transform.position + LocalPosition();

		private Vector3 LocalPosition() =>
				NormalAxis switch
				{
						Axis.X => XAxisPosition(),
						Axis.Y => YAxisPosition(),
						Axis.Z => ZAxisPosition(),
						_ => Vector3.zero
				};

		private Vector3 XAxisPosition()
		{
			Vector2 circle = PositionCircle();
			return new Vector3(0, circle.y, circle.x);
		}

		private Vector3 YAxisPosition()
		{
			Vector2 circle = PositionCircle();
			return new Vector3(circle.x, 0, circle.y);
		}

		private Vector3 ZAxisPosition()
		{
			Vector2 circle = PositionCircle();
			return new Vector3(circle.x, circle.y, 0);
		}

		private Vector2 PositionCircle() =>
				Random.insideUnitCircle * Radius;
	}
}
