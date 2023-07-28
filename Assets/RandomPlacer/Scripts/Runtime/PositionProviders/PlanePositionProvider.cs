using UnityEngine;

namespace RandomPlacer.PositionProviders
{
	public class PlanePositionProvider : PositionProvider
	{
		[field: SerializeField] public Vector2 Size { get; set; } = Vector2.one;

		[field: SerializeField] public Vector3 Center { get; set; } = Vector3.zero;

		[field: SerializeField] public Axis NormalAxis { get;  set; } = Axis.X;

		public override Vector3 GetPosition() =>
				transform.position + NormalAxis switch
				{
						Axis.X => XAxisPosition(),
						Axis.Y => YAxisPosition(),
						Axis.Z => ZAxisPosition(),
						_ => new Vector3()
				};

		private Vector3 XAxisPosition() =>
				new (
						Center.x, 
						Random.Range(-Size.y / 2, Size.y / 2), 
						Random.Range(-Size.x / 2, Size.x / 2)
				);
		
		private Vector3 YAxisPosition() =>
				new (
						Random.Range(-Size.x / 2, Size.x / 2), 
						Center.y, 
						Random.Range(-Size.y / 2, Size.y / 2)
				);
		
		private Vector3 ZAxisPosition() =>
				new (
						Random.Range(-Size.x / 2, Size.x / 2), 
						Random.Range(-Size.y / 2, Size.y / 2), 
						Center.z
				);
		
		public Vector3 GetInternalSizeByAxis() =>
				NormalAxis switch
				{
						Axis.X => new Vector3(Size.x, Size.y, 0),
						Axis.Y => new Vector3(Size.x, 0, Size.y),
						Axis.Z => new Vector3(0, Size.y, Size.x),
						_ => new Vector3()
				};
		
		public Vector2 ConvertSizeToAxis(Vector3 size) =>
				NormalAxis switch
				{
						Axis.X => new Vector2(size.x, size.y),
						Axis.Y => new Vector2(size.x, size.z),
						Axis.Z => new Vector2(size.y, size.z),
						_ => new Vector2()
				};
			
	}
}
