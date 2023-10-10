using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DataClasses
{
    [Serializable]
    public class TD_Saves : ModeSaveBase
    {
        private const string _fileName = "TD_Saves";

        public override string fileName => _fileName;

        [JsonProperty] public List<TD_SaveUnit> tD_Saves = new List<TD_SaveUnit>();

        public override SaveBase CreateInstance<T>()
        {
            return new TD_Saves();
        }
    }

    [Serializable]
    public record TD_SaveUnit
    {
        [JsonProperty] public string sceneName;
        [JsonProperty] public int record;
    }
}