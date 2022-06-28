using UnityEngine;

namespace Warlords.Utils
{
    public interface ISelectionView
    {
        GameObject gameObject { get; }
        UISelectionView SelectionView { get; }
    }
}