using UnityEngine;
using System.Collections;
using UniRx;

public static class EventExtensions
{
    /// <summary>
    /// Send a new message from the current game object
    /// </summary>
    /// <param name="trans">Current transformation</param>
    /// <param name="msg">Message to send</param>
    /// <typeparam name="T">Type of message to send</typeparam>
    public static void Send<T>(this GameObject go, T msg) where T : struct
    {
        EventPipe.Send(go, msg);
    }

    /// <summary>
    /// Send a new message from the current game object
    /// </summary>
    /// <param name="trans">Current transformation</param>
    /// <param name="msg">Message to send</param>
    /// <typeparam name="T">Type of message to send</typeparam>
    public static void Send<T>(this Transform trans, T msg) where T : struct
    {
        EventPipe.Send(trans.gameObject, msg);
    }

    /// <summary>
    /// Send a new message from the current game object
    /// </summary>
    /// <param name="trans">Current transformation</param>
    /// <param name="msg">Message to send</param>
    /// <typeparam name="T">Type of message to send</typeparam>
    public static void Send<T>(this MonoBehaviour behavior, T msg) where T : struct
    {
        EventPipe.Send(behavior.gameObject, msg);
    }

    /// <summary>
    /// Gets an observable stream for all messages of the specified type for a given game object
    /// </summary>
    /// <param name="go">Game object of interest</param>
    /// <typeparam name="T">Type of message to receive</typeparam>
    public static IObservable<T> Receive<T>(this GameObject go) where T : struct
    {
        return EventPipe.Receive<T>(go);
    }

    /// <summary>
    /// Gets an observable stream for all messages of the specified type for a given game object
    /// </summary>
    /// <param name="trans">Game object of interest</param>
    /// <typeparam name="T">Type of message to receive</typeparam>
    public static IObservable<T> Receive<T>(this Transform trans) where T : struct
    {
        return EventPipe.Receive<T>(trans.gameObject);
    }

    /// <summary>
    /// Gets an observable stream for all messages of the specified type for a given game object
    /// </summary>
    /// <param name="behavior">Game object of interest</param>
    /// <typeparam name="T">Type of message to receive</typeparam>
    public static IObservable<T> Receive<T>(this MonoBehaviour behavior) where T : struct
    {
        return EventPipe.Receive<T>(behavior.gameObject);
    }


    /// <summary>
    /// Gets an observable stream for all messages of the specified type for all game objects
    /// </summary>
    /// <param name="behavior">Behavior (not used)</param>
    /// <typeparam name="T">Type of message to receive</typeparam>
    public static IObservable<T> ReceiveAll<T>(this GameObject go) where T : struct
    {
        return EventPipe.Receive<T>();
    }

    /// <summary>
    /// Gets an observable stream for all messages of the specified type for all game objects
    /// </summary>
    /// <param name="behavior">Behavior (not used)</param>
    /// <typeparam name="T">Type of message to receive</typeparam>
    public static IObservable<T> ReceiveAll<T>(this Transform trans) where T : struct
    {
        return EventPipe.Receive<T>();
    }

    /// <summary>
    /// Gets an observable stream for all messages of the specified type for all game objects
    /// </summary>
    /// <param name="behavior">Behavior (not used)</param>
    /// <typeparam name="T">Type of message to receive</typeparam>
    public static IObservable<T> ReceiveAll<T>(this MonoBehaviour behavior) where T : struct
    {
        return EventPipe.Receive<T>();
    }
}