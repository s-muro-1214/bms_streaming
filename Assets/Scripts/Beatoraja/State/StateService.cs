using System;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public class StateService : WebSocketBehavior
{
    public static readonly string URI = "/state";

    public static BeatorajaState CurrentState { get; private set; }
    public static BeatorajaState PreviousState { get; private set; }

    protected override void OnMessage(MessageEventArgs e)
    {
        Enum.TryParse<BeatorajaState>(e.Data, out BeatorajaState newState);

        if (CurrentState == newState)
        {
            return;
        }

        PreviousState = CurrentState;
        CurrentState = newState;
    }
}
