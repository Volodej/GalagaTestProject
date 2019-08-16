using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace Units.Formation
{
    public class IncomingWaveQueueItem
    {
        public FormationLineSettings Line { get; }
        public List<FormationCell> CellsToReach { get; }

        public IncomingWaveQueueItem(FormationLineSettings line, List<FormationCell> cellsToReach)
        {
            Line = line;
            CellsToReach = cellsToReach;
        }
    }
}