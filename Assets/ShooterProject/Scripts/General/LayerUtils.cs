using UnityEngine;
namespace ShooterProject.Scripts.General
{
	public static class LayerUtils
	{
		public static bool IsLayerMatch(LayerMask sourceLayer, int testingLayer)
		{
			return (sourceLayer & (1 << testingLayer)) != 0;
		}
	}
}
