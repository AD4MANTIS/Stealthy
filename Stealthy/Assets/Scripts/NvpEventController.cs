using System;
using System.Collections.Generic;

namespace nvp.events
{
    public class NvpEventController
    {
        private static readonly Dictionary<string, GameEventWrapper> EventHandlers;

        static NvpEventController()
        {
            EventHandlers = new Dictionary<string, GameEventWrapper>();
        }

        public static GameEventWrapper Events(string eventName)
        {
            if (!EventHandlers.ContainsKey(eventName))
                EventHandlers[eventName] = new GameEventWrapper();

            return EventHandlers[eventName];
        }
    }

    public class GameEventWrapper
    {
        public event EventHandler GameEventHandler;

        public void TriggerEvent(object sender, EventArgs eventArgs)
        {
            GameEventHandler?.Invoke(sender, eventArgs);
        }
    }

    // Sample EventArgs
    public class DefaultEventArgs : EventArgs
    {
        public object Value;
    }
}