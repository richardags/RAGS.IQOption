using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using SuperSocket.ClientEngine;
using WebSocket4Net;
using RAGS.IQOption.WebRequest;
using RAGS.IQOption.WebSocket.Classes;
using RAGS.IQOption.WebSocket.Classes.JSON;
using RAGS.IQOption.WebSocket.Send.Classes;

namespace RAGS.IQOption.WebSocket
{
    public class WebSocketAPI : WebSocketResolver
    {
        /// <summary>
        /// Status of WebSocket connection.
        /// </summary>
        public enum Status
        {
            Connecting,
            Connected,
            IncorretCredentials,
            Closed,
            Error
        }

        /// <summary>
        /// E-mail of IQ Option account.
        /// </summary>
        public string email = null;
        /// <summary>
        /// Password of IQ Option account.
        /// </summary>
        public string password = null;

        private readonly string URI = "wss://iqoption.com/echo/websocket";
        private WebSocket4Net.WebSocket webSocket;
        private bool isDebugMode = false;

        /// <summary>
        /// Method only for inheritance.
        /// </summary>
        internal WebSocketAPI()
        {
            /* SslProtocols.Tls11 and SslProtocols.Tls deprecated 
             * webSocket = new WebSocket4Net.WebSocket(URI,
                sslProtocols: SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls);*/

            webSocket = new WebSocket4Net.WebSocket(URI, sslProtocols: SslProtocols.None);

            webSocket.Opened += new EventHandler((sender, e) => {
                OnSocketConnectionStatus(Status.Connected, null);
            });
            webSocket.Closed += new EventHandler((sender, e) => {
                OnSocketConnectionStatus(Status.Closed, "socket connection closed");
            });
            webSocket.Error += new EventHandler<ErrorEventArgs>((sender, e) => {
                OnSocketConnectionStatus(Status.Error, e.ToString());
            });
            webSocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>((sender, e) => {
                OnMessageReceived(e.Message);
            });
        }
        /// <summary>
        /// Establesh a connection via WebSocket
        /// </summary>
        internal void Open()
        {
            if (webSocket.State == WebSocketState.None ||
                webSocket.State == WebSocketState.Closed)
            {
                OnSocketConnectionStatus(Status.Connecting, null);
                webSocket.Open();
            }
        }
        internal void Close()
        {
            if (webSocket.State == WebSocketState.Connecting ||
                webSocket.State == WebSocketState.Open)
            {
                webSocket.Close();
            }
        }
        internal bool IsStatusClosed
        {
            get
            {
                return webSocket.State == WebSocketState.None ||
                    webSocket.State == WebSocketState.Closed;
            }
        }

        private void OnSocketConnectionStatus(Status status, string error_message)
        {
            if (isDebugMode)
            {
                Console.WriteLine(string.Format("OnConnectionStatus Status({0}) Message({1})",
                    status, error_message));
            }

            switch (status)
            {
                case Status.Connecting:
                    OnAuthenticationStatus(Status.Connecting);
                    break;
                case Status.Connected:
                    WebRequestAPI.GetSSIDResult ssidResult = SSIDManager.Get(email, password, true);

                    if (ssidResult.error == null)
                    {
                        Send(Authenticate.Send(ssidResult.ssid));
                    }
                    else
                    {
                        if (ssidResult.isCache)
                        {
                            ssidResult = SSIDManager.Get(email, password, false);

                            if (ssidResult.error == null)
                            {
                                Send(Authenticate.Send(ssidResult.ssid));
                                return;
                            }
                        }

                        if (ssidResult.error.code == WebRequestAPI.GetSSIDResult.IncorretCredentials)
                        {
                            OnAuthenticationStatus(Status.IncorretCredentials);
                        }
                        else
                        {
                            string error = string.Format("WebSocketAPI.cs OnSocketConnectionStatus() - error: {0} {1}",
                                ssidResult.error.code, ssidResult.error.message);
                            OnAuthenticationStatus(Status.Error, error);

                            if (isDebugMode)
                            {
                                Console.WriteLine(error);
                            }
                        }
                    }
                    break;
                case Status.Closed:
                    OnAuthenticationStatus(Status.Closed);
                    break;
                case Status.Error:
                    OnAuthenticationStatus(Status.Error);
                    break;
            }
        }
        internal void Send(string message)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                webSocket.Send(message);
            }
        }
        private void OnMessageReceived(string message)
        {
            if (isDebugMode)
            {
                Console.WriteLine(message);
            }

            ResolveMessage(message);            
        }

        internal override void OnAuthenticatedEvent(bool isAuthenticated)
        {
            if (isAuthenticated)
            {
                OnAuthenticationStatus(Status.Connected);
            }
            else
            {
                SSIDManager.Delete(email);
                OnAuthenticationStatus(Status.IncorretCredentials, "SSID revoked!");
            }
        }

        internal virtual void OnAuthenticationStatus(Status status, string error_message = null) { }

        protected new virtual void SetDebugMode(bool enabled)
        {
            base.SetDebugMode(enabled);
            isDebugMode = enabled;
        }
    }
}