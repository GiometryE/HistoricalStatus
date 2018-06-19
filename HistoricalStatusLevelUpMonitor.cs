using ICities;

namespace HistoricalStatus
{
    public class HistoricalStatusLevelUpMonitor : LevelUpExtensionBase
    {
        public override ResidentialLevelUp OnCalculateResidentialLevelUp(ResidentialLevelUp levelUp, int averageEducation, int landValue,
            ushort buildingID, Service service, SubService subService, Level currentLevel)
        {
            if (HistoricalStatusDataManager.Instance.IsHistorical(buildingID))
            {
                levelUp.targetLevel = currentLevel;
            }

            return levelUp;
        }

        public override CommercialLevelUp OnCalculateCommercialLevelUp(CommercialLevelUp levelUp, int averageWealth, int landValue,
            ushort buildingID, Service service, SubService subService, Level currentLevel)
        {
            if (HistoricalStatusDataManager.Instance.IsHistorical(buildingID))
            {
                levelUp.targetLevel = currentLevel;
            }

            return levelUp;
        }

        public override IndustrialLevelUp OnCalculateIndustrialLevelUp(IndustrialLevelUp levelUp, int averageEducation, int serviceScore,
            ushort buildingID, Service service, SubService subService, Level currentLevel)
        {
            if (HistoricalStatusDataManager.Instance.IsHistorical(buildingID))
            {
                levelUp.targetLevel = currentLevel;
            }

            return levelUp;
        }

        public override OfficeLevelUp OnCalculateOfficeLevelUp(OfficeLevelUp levelUp, int averageEducation, int serviceScore, ushort buildingID,
            Service service, SubService subService, Level currentLevel)
        {
            if (HistoricalStatusDataManager.Instance.IsHistorical(buildingID))
            {
                levelUp.targetLevel = currentLevel;
            }

            return levelUp;
        }
    }
}
