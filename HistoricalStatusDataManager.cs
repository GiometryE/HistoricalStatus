using ColossalFramework.IO;
using ICities;
using System.IO;
using UnityEngine;

namespace HistoricalStatus
{
    public class HistoricalStatusDataManager : SerializableDataExtensionBase
    {
        // Singleton getter
        public static HistoricalStatusDataManager Instance { get; private set; }

        // The data object of our mod
        private HistoricalStatusData _data;


        public bool IsHistorical(ushort buildingId)
        {
            return _data.HistoricalBuildingIds.Contains(buildingId);
        }

        public void AddBuildingId(ushort buildingId)
        {
            if (_data.HistoricalBuildingIds.Contains(buildingId)) return;

            _data.HistoricalBuildingIds.Add(buildingId);

            Debug.Log($"Historical Building {buildingId} added");
        }

        public void RemoveBuildingId(ushort buildingId)
        {
            if (!_data.HistoricalBuildingIds.Contains(buildingId)) return;

            _data.HistoricalBuildingIds.Remove(buildingId);

            Debug.Log($"Historical Building {buildingId} removed");
        }


        public override void OnCreated(ISerializableData serializedData)
        {
            base.OnCreated(serializedData);

            Instance = this; // initialize singleton
        }

        public override void OnReleased()
        {
            Instance = null; // reset singleton
        }


        public override void OnLoadData()
        {
           // Get bytes from savegame
            byte[] bytes = serializableDataManager.LoadData(HistoricalStatusData.DataId);
            if (bytes != null)
            {
                // Convert the bytes to HistoricalStatusData object
                using (var stream = new MemoryStream(bytes))
                {
                    _data = DataSerializer.Deserialize<HistoricalStatusData>(stream, DataSerializer.Mode.Memory);
                }

                Debug.LogFormat("Data loaded (Size in bytes: {0})", bytes.Length);
            }
            else
            {
                _data = new HistoricalStatusData();

                Debug.Log("Data created.");
            }
        }

        public override void OnSaveData()
        {
            byte[] bytes;

            // Convert the HistoricalStatusHistoricalStatusData object to bytes
            using (var stream = new MemoryStream())
            {
                DataSerializer.Serialize(stream, DataSerializer.Mode.Memory, HistoricalStatusData.DataVersion, _data);
                bytes = stream.ToArray();
            }

            // Save bytes in savegame
            serializableDataManager.SaveData(HistoricalStatusData.DataId, bytes);

            Debug.LogFormat("Data Saved (Size in bytes: {0})", bytes.Length);
        }
    }
}
