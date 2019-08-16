using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Formation
{
    /// <summary>
    /// Stores relative position to the formation center: from -0.5 to 0.5 for each axis
    /// </summary>
    public class FormationCell : MonoBehaviour
    {
        [SerializeField] private float _lineRelativePosition;
        [SerializeField] private float _columnRelativePosition;

        public float LineRelativePosition => _lineRelativePosition;
        public float ColumnRelativePosition => _columnRelativePosition;

        public static FormationCell Create(Transform parent, int lineIndex, int columnIndex, int linesCount, int enemiesInLine,
            int maxRowsCount)
        {
            var go = new GameObject($"{lineIndex}_{columnIndex}");
            go.transform.SetParent(parent);
            var cell = go.AddComponent<FormationCell>();

            cell._lineRelativePosition = ((float) linesCount - lineIndex) / linesCount - 0.5f;
            cell._columnRelativePosition = (float) enemiesInLine / maxRowsCount * (((float) columnIndex + 1) / enemiesInLine - 0.5f);
            return cell;
        }
    }
}