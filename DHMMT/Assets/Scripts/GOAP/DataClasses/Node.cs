using SO.GOAP;
using System.Collections.Generic;

namespace GOAP.GoapDataClasses
{
    public class Node
    {
        public Node parent;
        public float cost;
        public Dictionary<GOAPStrings, int> state;
        public GAction action;

        public Node(Node parent, float cost, Dictionary<GOAPStrings, int> allStates, GAction action)
        {
            this.parent = parent;
            this.cost = cost;
            this.state = new Dictionary<GOAPStrings, int>(allStates);
            this.action = action;
        }

        public Node(Node parent, float cost, Dictionary<GOAPStrings, int> allStates, Dictionary<GOAPStrings, int> beliefStates, GAction action)
        {
            this.parent = parent;
            this.cost = cost;
            this.state = new Dictionary<GOAPStrings, int>(allStates);

            foreach (KeyValuePair<GOAPStrings, int> b in beliefStates)
            {
                if (!this.state.ContainsKey(b.Key))
                {
                    this.state.Add(b.Key, b.Value);
                }
            }

            this.action = action;
        }
    }
}