using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace tcppalvelin
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Loopback, 8888);
            s.Bind(iep);
            s.Listen(1);
            Socket asiakas = s.Accept();
            byte[] rec = new byte[2048];
            asiakas.Receive(rec);
            //Console.Write(Encoding.UTF8.GetString(rec));
            string a = "Vilin palvelin: " + Encoding.UTF8.GetString(rec);
            Console.Write(a);
            //string msg = "GET " + resurssi + " HTTP/1.1\r\nHost:" + host + "\r\n\r\n";
            byte[] snd = Encoding.UTF8.GetBytes(a);
            asiakas.Send(snd);
            asiakas.Close();
            Console.ReadKey();
            s.Close();
        }
    }
}
