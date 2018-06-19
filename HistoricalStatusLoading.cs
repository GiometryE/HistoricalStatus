using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace HistoricalStatus
{
    public class HistoricalStatusLoading : LoadingExtensionBase
    {
        private UICheckBox _HistoricalStatusCheckBox;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (_HistoricalStatusCheckBox != null) return;

            ZonedBuildingWorldInfoPanel panel = UIView.library.Get<ZonedBuildingWorldInfoPanel>(typeof(ZonedBuildingWorldInfoPanel).Name);
            UICheckBox checkBox = panel.component.AddUIComponent<UICheckBox>();

            checkBox.width = panel.component.width;
            checkBox.height = 20f;
            checkBox.clipChildren = true;

            UISprite sprite = checkBox.AddUIComponent<UISprite>();
            sprite.spriteName = "ToggleBase";
            sprite.size = new Vector2(16f, 16f);
            sprite.relativePosition = Vector3.zero;

            checkBox.checkedBoxObject = sprite.AddUIComponent<UISprite>();
            ((UISprite)checkBox.checkedBoxObject).spriteName = "ToggleBaseFocused";
            checkBox.checkedBoxObject.size = new Vector2(16f, 16f);
            checkBox.checkedBoxObject.relativePosition = Vector3.zero;

            checkBox.label = checkBox.AddUIComponent<UILabel>();
            checkBox.label.text = " ";
            checkBox.label.textScale = 0.9f;
            checkBox.label.relativePosition = new Vector3(22f, 2f);

            checkBox.name = "HistoricalStatus";
            checkBox.text = "Historical Status";

            checkBox.relativePosition = new Vector3(14f, 164f + 130f + 5f);

            panel.component.height = 321f + 5f + 16f;

            _HistoricalStatusCheckBox = checkBox;

            checkBox.eventCheckChanged += (component, check) =>
            {
                ushort buildingId = WorldInfoPanel.GetCurrentInstanceID().Building;

                if (check)
                {
                    HistoricalStatusDataManager.Instance.AddBuildingId(buildingId);
                }
                else
                {
                    HistoricalStatusDataManager.Instance.RemoveBuildingId(buildingId);
                }
            };
        }
    }
}
