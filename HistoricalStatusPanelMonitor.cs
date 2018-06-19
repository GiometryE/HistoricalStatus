using ColossalFramework.UI;
using ICities;

namespace HistoricalStatus
{
    public class HistoricalStatusPanelMonitor : ThreadingExtensionBase
    {
        private ZonedBuildingWorldInfoPanel _panel;
        private UICheckBox _HistoricalStatusCheckBox;

        private ushort _lastBuildingId = 0;

        // called every frame
        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            if (!FindComponents()) return;

            if (_panel.component.isVisible)
            {
                ushort buildingId = WorldInfoPanel.GetCurrentInstanceID().Building;

                if (_lastBuildingId != buildingId)
                {
                    _HistoricalStatusCheckBox.isChecked = HistoricalStatusDataManager.Instance.IsHistorical(buildingId);
                    _lastBuildingId = buildingId;
                }
            }
            else
            {
                _lastBuildingId = 0;
            }
        }

        private bool FindComponents()
        {
            if (_panel != null && _HistoricalStatusCheckBox != null) return true;

            _panel = UIView.library.Get<ZonedBuildingWorldInfoPanel>(typeof(ZonedBuildingWorldInfoPanel).Name);
            if (_panel == null) return false;

            _HistoricalStatusCheckBox = _panel.component.Find<UICheckBox>("HistoricalStatus");
            return _HistoricalStatusCheckBox != null;
        }
    }
}
