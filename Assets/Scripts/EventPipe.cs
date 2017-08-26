using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;
using System.Linq;

public static class EventPipe
{
    static readonly Dictionary<Type, ISubject<Message>> _subjectMap = new Dictionary<Type, ISubject<Message>>();

    public static bool Logging { get; set; }

    static EventPipe()
    {
        //Load the previous settings to enable or disable logging
        Logging = true;
    }

    static ISubject<Message> Pipe<T>() where T : struct
    {
        ISubject<Message> pipe;

        var type = typeof(T);

        //If the pipe for this message type doesnt exist yet, create it
        if (!_subjectMap.TryGetValue(type, out pipe))
        {
            //Attempt to get the message attribute for this message type
            var msgAttribute = type.GetCustomAttributes(typeof(CheesyMessage), true).FirstOrDefault() as CheesyMessage;

            //If there is no attribute, or the messages should not be queued, create a normal subject
            if (msgAttribute == null || !msgAttribute.LatestOnSubscribe)
                pipe = new Subject<Message>();
            //Otherwise create a behavior subject to send the latest message when it is subscribed
            else
                pipe = new BehaviorSubject<Message>(null);

            _subjectMap.Add(type, pipe);
        }

        return pipe;
    }

    /// <summary>
    /// Send a message from a specific game object
    /// </summary>
    /// <param name="go">Originating GameObject</param>
    /// <param name="content">Message contents</param>
    /// <typeparam name="T">Type of message to send</typeparam>
    public static void Send<T>(GameObject go, T content) where T : struct
    {
        if (go == null)
        {
            Debug.LogError("Can not send messages with a null origin GameObject");
            return;
        }

        //Get the pipe for this type of message
        var pipe = Pipe<T>();

        //Create a new message to send
        var msg = new Message(go, content);

        //Log, if necessary
        if (Logging)
            Debug.Log(msg);

        //Creates and sends the new message with the specified content
        pipe.OnNext(msg);
    }

    /// <summary>
    /// Gets an observable stream for all messages of the specified type for a given game object
    /// </summary>
    /// <param name="go">Game object of interest</param>
    /// <typeparam name="T">Type of message to receive</typeparam>
    public static IObservable<T> Receive<T>(GameObject go) where T : struct
    {
        if (go == null)
        {
            Debug.LogWarning(string.Format("Attempting to receive {0} messages for a null game object, this will never be valid.", typeof(T)));
        }

        //Get the pipe for this type of message
        var pipe = Pipe<T>();

        //Return a sequence that filters by the game object,
        //and converts messages to the core message data type
        return pipe.Where(i => i.Sender.Equals(go))
            .Select(i => i.Content)
                .OfType<object, T>();
    }

    /// <summary>
    /// Gets an observable stream for all messages of the specified type
    /// </summary>
    /// <typeparam name="T">Type of message to receive</typeparam>
    public static IObservable<T> Receive<T>() where T : struct
    {
        //Get the pipe for this type of message
        var pipe = Pipe<T>();

        //Return for all messages and converts them to the core message data type
        return pipe.Select(i => i.Content).OfType<object, T>();
    }
}