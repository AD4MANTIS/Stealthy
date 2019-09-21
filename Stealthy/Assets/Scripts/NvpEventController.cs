using System;
using System.Collections.Generic;

namespace nvp.events
{
    public enum MyEvent
    {
        Null = 0,
        PlayerSeesEnemy,
        EnemySeesPlayer,
        PlayerDies,
        OnHitByPlayer,
        LevelFinish,
        HideInBox
    }

    public class NvpEventController
    {
        private static readonly Dictionary<MyEvent, GameEventWrapper> EventHandlers;

        static NvpEventController()
        {
            EventHandlers = new Dictionary<MyEvent, GameEventWrapper>();
        }

        public static GameEventWrapper Events(MyEvent eventName)
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