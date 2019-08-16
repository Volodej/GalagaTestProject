using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace Units.Formation
{
    public class FormationExpander : MonoBehaviour
    {
        private FormationSettings _formationSettings;
        private List<FormationCell> _formationCells;
        private Vector2 _minSize;
        private Vector2 _maxSize;

        public static FormationExpander AttachTo(GameObject go, FormationSettings formationSettings, List<FormationCell> formationCells,
            int linesCount, int maxColumnsCount)
        {
            var expander = go.AddComponent<FormationExpander>();
            expander._formationSettings = formationSettings;
            expander._formationCells = formationCells;
            expander._minSize =
                GetFormationSize(linesCount, maxColumnsCount, formationSettings.UnitSize, formationSettings.MinUnitsDistance);
            expander._maxSize =
                GetFormationSize(linesCount, maxColumnsCount, formationSettings.UnitSize, formationSettings.MaxUnitsDistance);
            return expander;
        }

        private static Vector2 GetFormationSize(int lines, int columns, int unitSize, int distance)
        {
            return new Vector2(columns * unitSize + (columns - 1) * distance, lines * unitSize + (lines - 1) * distance);
        }

        private void Update()
        {
            var expansionFactor = Mathf.Max(Mathf.Sin(Time.time / _formationSettings.SqueezeExpandCycleTime * Mathf.PI), 0);
            var currentSize = Vector2.Lerp(_minSize, _maxSize, expansionFactor);
            foreach (var cell in _formationCells)
            {
                cell.transform.localPosition = new Vector3(cell.ColumnRelativePosition * currentSize.x,
                    cell.LineRelativePosition * currentSize.y, 0);
            }
        }
    }
}