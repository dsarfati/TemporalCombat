using UnityEngine;
using System.Collections;

public sealed class CheesyMessage : System.Attribute
{
    /// <summary>
    /// Should the latest message be sent to a newly subscribed observer?
    /// </summary>
    public bool LatestOnSubscribe { get; set; }
}

/// <summary>
/// Internal messages sent within the CheesyEvents system
/// </summary>
public sealed class Message
{
    /// <summary>
    /// GameObject that sent the message
    /// </summary>
    public GameObject Sender { get; private set; }

    /// <summary>
    /// Core message data
    /// </summary>
    public object Content { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    /// <param name="go">GameObject that sent the message</param>
    /// <param name="content">Data contained within the message</param>
    public Message(GameObject sender, object content)
    {
        this.Sender = sender;
        this.Content = content;
    }

    public override string ToString()
    {
        return string.Format("CheesyEvent: Sender={0}, Content={1}", Sender.name, Content);
    }
}