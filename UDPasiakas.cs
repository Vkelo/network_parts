using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace tcppalvelin
{
    class Udpasiakas // Udp asiakkaan ja palvelimen ero on se, kumpi lähettää ensin dataa, ei muuta eroa.
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint vastaanottaja = new IPEndPoint(IPAddress.Loopback, 8888);
            string a = "Vilin palvelin";
            byte[] snd = Encoding.UTF8.GetBytes(a);
            s.SendTo(snd, vastaanottaja);
            byte[] rec = new byte[2048];
            IPEndPoint vast = new IPEndPoint(IPAddress.Loopback, 8888);
            s.Receive(rec, ref (EndPoint)vast);
            Console.ReadKey();
            s.Close();


        }
    }
}
