using UnityEngine;
using WebSocketSharp.Server;

public class BeatorajaWatcher : MonoBehaviour
{ 
    private WebSocketServer _server;

    private void Start()
    {
        IBeatorajaManager manager = ServiceLocator.GetInstance<IBeatorajaManager>();
        _server = new WebSocketServer(manager.GetWebSocketPort());
        _server.AddWebSocketService<RandomPatternService>(RandomPatternService.URI);
        _server.AddWebSocketService<StateService>(StateService.URI);

        _server.Start();
    }

    private void OnDestroy()
    {
        _server.Stop();
        _server = null;
    }
}
