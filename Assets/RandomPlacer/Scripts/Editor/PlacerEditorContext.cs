using System;

using RandomPlacer.PositionProviders;
using RandomPlacer.ScaleProviders;

using UnityEngine;

using Object = UnityEngine.Object;

namespace RandomPlacer
{
	public class PlacerEditorContext
	{
		private BoundsType _lastBoundsType;
		private RotationType _rotationType;
		private ScaleType _scaleType;

		private PositionProvider _currentPositionProvider;
		private RotationProvider _currentRotationProvider;
		private ScaleProvider _currentScaleProvider;
		private readonly Placer _placer;

		public PlacerEditorContext(Placer placer) =>
				_placer = placer;

		public void Replace() =>
				_placer.Replace();

		public void Clear() =>
				_placer.Clear();

		public void Place() =>
				_placer.Place();

		public void Validate()
		{
			ValidatePositionProvider();
			ValidateRotationProvider();
			ValidateScaleProvider();
		}

		private void ValidateScaleProvider()
		{
			ScaleType scaleType = _placer.ScaleType;

			bool notNeedSwitch = _scaleType == scaleType;

			if (notNeedSwitch)
				return;

			_scaleType = scaleType;

			GameObject gameObject = _placer.gameObject;

			_currentScaleProvider = ResolveScaleProvider(gameObject, scaleType);
		}

		private ScaleProvider ResolveScaleProvider(GameObject gameObject, ScaleType scaleType) =>
				scaleType switch
				{
						ScaleType.Uniform => SwitchScaleProvider<UniformScaleProvider>(gameObject),
						ScaleType.NonUniform => SwitchScaleProvider<NonUniformScaleProvider>(gameObject),
						_ => throw new ArgumentOutOfRangeException(nameof(scaleType), scaleType, null)
				};

		private TScaleProvider SwitchScaleProvider<TScaleProvider>
				(GameObject gameObject) where TScaleProvider : ScaleProvider
		{
			Object.DestroyImmediate(_currentScaleProvider);

			return gameObject.GetComponent<TScaleProvider>() ?? gameObject.AddComponent<TScaleProvider>();
		}

		private void ValidateRotationProvider()
		{
			RotationType rotationType = _placer.RotationType;

			bool notNeedSwitch = _rotationType == rotationType;

			if (notNeedSwitch)
				return;

			_rotationType = rotationType;

			GameObject gameObject = _placer.gameObject;

			_currentRotationProvider = ResolveRotationProvider(gameObject, rotationType);
		}

		private RotationProvider ResolveRotationProvider(GameObject gameObject, RotationType rotationType) =>
				rotationType switch
				{
						RotationType.NoRotation => SwitchRotationProvider<NoRotationProvider>(gameObject),
						RotationType.Uniform => SwitchRotationProvider<UniformRotationProvider>(gameObject),
						RotationType.UniformX => SwitchRotationProvider<UniformXRotationProvider>(gameObject),
						RotationType.UniformY => SwitchRotationProvider<UniformYRotationProvider>(gameObject),
						RotationType.UniformZ => SwitchRotationProvider<UniformZRotationProvider>(gameObject),
						RotationType.UniformXY => SwitchRotationProvider<UniformXYRotationProvider>(gameObject),
						RotationType.UniformXZ => SwitchRotationProvider<UniformXZRotationProvider>(gameObject),
						RotationType.UniformYZ => SwitchRotationProvider<UniformYZRotationProvider>(gameObject),
						_ => throw new ArgumentOutOfRangeException()
				};

		private TRotationProvider SwitchRotationProvider<TRotationProvider>(GameObject gameObject)
				where TRotationProvider : RotationProvider
		{
			Object.DestroyImmediate(_currentRotationProvider);
			return gameObject.GetComponent<TRotationProvider>() ?? gameObject.AddComponent<TRotationProvider>();
		}

		private void ValidatePositionProvider()
		{
			BoundsType boundsType = _placer.BoundsType;

			bool notNeedSwitch = _lastBoundsType == boundsType;

			if (notNeedSwitch)
				return;

			_lastBoundsType = boundsType;

			GameObject gameObject = _placer.gameObject;

			_currentPositionProvider = ResolvePositionProvider(gameObject, boundsType);
		}

		private PositionProvider ResolvePositionProvider(GameObject gameObject, BoundsType placerBoundsType) =>
				placerBoundsType switch
				{
						BoundsType.Point => SwitchPositionProvider<PointPositionProvider>(gameObject),
						BoundsType.Box => SwitchPositionProvider<BoxPositionProvider>(gameObject),
						BoundsType.Sphere => SwitchPositionProvider<SpherePositionProvider>(gameObject),
						BoundsType.Circle => SwitchPositionProvider<CirclePositionProvider>(gameObject),
						BoundsType.Plane => SwitchPositionProvider<PlanePositionProvider>(gameObject),
						_ => throw new ArgumentOutOfRangeException()
				};

		private TPositionProvider SwitchPositionProvider<TPositionProvider>(GameObject placer)
				where TPositionProvider : PositionProvider
		{
			Object.DestroyImmediate(_currentPositionProvider);
			return placer.GetComponent<TPositionProvider>() ?? placer.AddComponent<TPositionProvider>();
		}
	}
}