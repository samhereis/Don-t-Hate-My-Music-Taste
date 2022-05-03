using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public static class ComponentHelper
    {
        public static void AssignIfNull(this Component component, Component newValue)
        {
            if (component == null) component = newValue;
        }
    }
}