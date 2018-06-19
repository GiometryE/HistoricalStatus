using ICities;
using UnityEngine;

namespace HistoricalStatus
{
    public class HistoricalStatusBuildingMonitor : BuildingExtensionBase
    {
        public override void OnBuildingReleased(ushort id)
        {
            if (HistoricalStatusDataManager.Instance.IsHistorical(id))
            {
                // Remove demolished/destroyed buildings from the list of historical buildings
                HistoricalStatusDataManager.Instance.RemoveBuildingId(id);

                Debug.Log($"Historical Building {id} was released.");
            }
        }
    }
}
