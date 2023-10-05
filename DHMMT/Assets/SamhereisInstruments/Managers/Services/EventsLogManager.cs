using Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class EventsLogManager : MonoBehaviour, IInitializable
    {
        private static bool _isDataCollectionEnabled = false;

        public void Initialize()
        {

        }

        public void SetDataCollectionStatus(bool collect)
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        public static void LogEvent(string name, string parameterName, string parameterValue)
        {
            if (_isDataCollectionEnabled == false) return;

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { parameterName, parameterValue}
            };

            LogEvent(name, parameters);
        }

        public static void LogEvent(string name, string parameterName, float parameterValue)
        {
            if (_isDataCollectionEnabled == false) return;

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { parameterName, parameterValue}
            };

            LogEvent(name, parameters);
        }

        public static void LogEvent(string name, string parameterName, int parameterValue)
        {
            if (_isDataCollectionEnabled == false) return;

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { parameterName, parameterValue}
            };

            LogEvent(name, parameters);
        }

        public static void LogEvent(string name)
        {
            if (_isDataCollectionEnabled == false) return;

            try
            {

            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        public static void LogEvent(string name, Dictionary<string, object> parameters)
        {
            if (_isDataCollectionEnabled == false) return;

            try
            {

            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }
}