using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApplication1
{
    public class WebSocketSession
    {
        private readonly WebSocket _webSocket;
        private int _balance = 1000;
        private List<Bet> _bets = new List<Bet>();

        public WebSocketSession(WebSocket socket)
        {
            _webSocket = socket;
        }

        public async Task OnRoll(int num)
        {
            int win = 0;
           // Console.WriteLine("betsCount{0}",_bets.Count);
            foreach (Bet bet in _bets)
            {
                win += bet.GetWin(num);
            }
            _bets.Clear();
            string res = ("{\"value\": " + num + "}");
            byte[] response = Encoding.UTF8.GetBytes(res);
            await _webSocket.SendAsync(new ArraySegment<byte>(response, 0, res.Length), WebSocketMessageType.Text, true,
                CancellationToken.None);
//            Thread.Sleep(4000);
            res = ("{\"win\": " + win + "}");
            response = Encoding.UTF8.GetBytes(res);
            await _webSocket.SendAsync(new ArraySegment<byte>(response, 0, res.Length), WebSocketMessageType.Text, true,
                CancellationToken.None);
        }

        public async Task Listen()
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result =
                await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string response = Encoding.UTF8.GetString(buffer);
                try
                {
                    _bets.Add(JsonConvert.DeserializeObject<Bet>(response));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}