using SO.GOAP;
using System.Collections.Generic;

namespace GOAP.GoapDataClasses
{
    [System.Serializable]
    public class GoapState
    {
        public GOAPStrings key;
        public int value;
    }

    public class GoapStates
    {
        public Dictionary<GOAPStrings, int> goapStates = new Dictionary<GOAPStrings, int>();

        public bool HasState(GOAPStrings key)
        {
            return goapStates.ContainsKey(key);
        }

        private void AddState(GOAPStrings key, int value)
        {
            goapStates.Add(key, value);
        }

        public void ModifyState(GOAPStrings key, int value)
        {
            if (HasState(key))
            {
                goapStates[key] += value;

                if (goapStates[key] <= 0)
                {
                    RemoveState(key);
                }
            }
            else
            {
                AddState(key, value);
            }
        }

        public void RemoveState(GOAPStrings key)
        {
            if (HasState(key))
            {
                goapStates.Remove(key);
            }
        }

        public void SetState(GOAPStrings key, int value)
        {
            if (HasState(key))
            {
                goapStates[key] = value;
            }
            else
            {
                AddState(key, value);
            }
        }

        public Dictionary<GOAPStrings, int> GetStates()
        {
            return goapStates;
        }
    }
}