using System;

namespace Spectometer
{
  public static   class EventHandlerExtensions

    {
        public static void Raise<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
        {
            // Copy to temp var to be thread-safe (taken from C# 3.0 Cookbook - don't know if it's true)
            EventHandler<T> copy = handler;
            if (copy != null)
            {
                copy(sender, args);
            }
        }
    }
}
