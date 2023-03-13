using System;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public class StateService : WebSocketBehavior
{
    public static readonly string URI = "/state";

    private static BeatorajaState _state = BeatorajaState.NONE;

    protected override void OnMessage(MessageEventArgs e)
    {
        Enum.TryParse<BeatorajaState>(e.Data, out _state);
    }

    public static BeatorajaState GetState()
    {
        return _state;
    }
}
