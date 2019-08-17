using LifeCycle.Level;
using Settings;
using UnityEngine;

namespace Units.Formation
{
    public class FormationMover : MonoBehaviour
    {
        [SerializeField] private Bounds _centerBounds;
        private FormationSettings _formationSettings;
        private float _startTime;
        private LevelSettings _levelSettings;

        public static FormationMover AttachTo(GameObject go, FormationSettings formationSettings, LevelSettings levelSettings,
            int linesCount, int maxColumnsCount)
        {
            var mover = go.AddComponent<FormationMover>();
            mover._formationSettings = formationSettings;
            mover._startTime = Time.time;
            mover._levelSettings = levelSettings;
            mover._centerBounds = CalculateCenterBounds(formationSettings.AllowedFormationBounds, formationSettings.UnitSize,
                formationSettings.MaxUnitsDistance, linesCount, maxColumnsCount);
            return mover;
        }


        private void Update()
        {
            var timeFromStart = Time.time - _startTime;
            var xPosition = _centerBounds.center.x + Mathf.Lerp(_centerBounds.min.x, _centerBounds.max.x,
                                Mathf.Sin(timeFromStart / _levelSettings.HorizontalMovementTime * Mathf.PI) / 2 + 0.5f);
            var yPosition = Mathf.Lerp(_centerBounds.max.y, _centerBounds.min.y,
                Mathf.Clamp01(timeFromStart / _levelSettings.VerticalMovementTime));
            transform.position = new Vector3(xPosition, yPosition, 0);
        }


        private static Bounds CalculateCenterBounds(Bounds totalBounds, int unitSize, int maxUnitsDistance, int rowsCount,
            int maxUnitsInRow)
        {
            var maxFormationWidth = unitSize * maxUnitsInRow + (maxUnitsDistance - 1) * maxUnitsInRow;
            var maxFormationHeight = unitSize * rowsCount + (maxUnitsDistance - 1) * rowsCount;
            var maxFormationSize = new Vector3(maxFormationWidth, maxFormationHeight);
            return new Bounds(totalBounds.center, totalBounds.size - maxFormationSize);
        }
    }
}