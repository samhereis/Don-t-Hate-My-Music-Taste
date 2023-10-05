using SO.GOAP;
using System.Collections.Generic;

namespace GOAP.GoapDataClasses
{
    public class SubGoals
    {
        public Dictionary<GOAPStrings, int> subGoals;
        public bool remove;

        public SubGoals(Dictionary<GOAPStrings, int> goal, bool r)
        {
            subGoals = goal;
            remove = r;
        }
    }
}