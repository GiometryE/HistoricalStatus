using System.Collections.Generic;
using System.Linq;
using ColossalFramework.IO;

namespace HistoricalStatus
{
    public class HistoricalStatusData : IDataContainer
    {
        // The key for our data in the savegame
        public const string DataId = "HistoricalStatus";

        // Version of data save format
        // This is important when you add new fields to HistoricalStatusData
        public const int DataVersion = 0;


        public List<ushort> HistoricalBuildingIds { get; set; } = new List<ushort>();


        // This serializes the object (to bytes)
        public void Serialize(DataSerializer s)
        {
            // convert ushort list to int array
            int[] ids = HistoricalBuildingIds.Select(id => (int)id).ToArray();

            s.WriteInt32Array(ids);
        }

        // This reads the object (from bytes)
        public void Deserialize(DataSerializer s)
        {
            int[] ids = s.ReadInt32Array();

            // convert int array to ushort list
            HistoricalBuildingIds = ids.Select(id => (ushort)id).ToList();
        }

        // Validates that all building ids are active
        public void AfterDeserialize(DataSerializer s)
        {
            if (!BuildingManager.exists) return;

            List<ushort> validatedBuildingIds = new List<ushort>();

            Building[] buildingInstances = BuildingManager.instance.m_buildings.m_buffer;

            // itertate through all building ids, filter active ids
            foreach (ushort buildingId in HistoricalBuildingIds)
            {
                if (buildingInstances[buildingId].m_flags != Building.Flags.None)
                {
                    validatedBuildingIds.Add(buildingId);
                }
            }

            HistoricalBuildingIds = validatedBuildingIds;
        }
    }
}
