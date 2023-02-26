using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
public class TCPasiakas
{
    public static void Main()
    {
        Socket soketti = null;
        NetworkStream ns = null;
        //StreamReader sr = null;
        StreamWriter sw = null;
        try
        {

            soketti = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soketti.Connect("www.example.com", 80);
            ns = new NetworkStream(soketti);
            string snd = "GET " + "/" + " HTTP/1.1\r\nHost:" + "www.example.com" + "\r\n\r\n";
            sw = new StreamWriter(ns);
            sw.Write(snd);  
            sw.Flush();
            byte[] rec = new byte[2048]; 
            int paljon = soketti.Receive(rec);
            String vastaus = System.Text.Encoding.ASCII.GetString(rec, 0, paljon);


            Console.WriteLine("Soketti luotiin onnistuneesti seuraavin ominaisuuksin:\r\n"
            + "AddressFamily = " + soketti.AddressFamily.ToString() + "\r\nSocketType = "
            + soketti.SocketType.ToString() + "\r\nProtocolType = " + soketti.ProtocolType.ToString());
            IPEndPoint Aiep = (IPEndPoint)soketti.RemoteEndPoint;
            Console.WriteLine("Soketti yhdistettiin palvelimeen: {0} porttiin {1}", Aiep.Address, Aiep.Port);
            Console.WriteLine("Vastaus palvelimelta:\r\n" + vastaus);
        }
        catch (SocketException se)
        {
            Console.WriteLine(se.Message);
            if (se.Message.StartsWith("Could not resolve host"))
            {
                Console.WriteLine("DNS palvelu selvittää verkkotunnuksia, osoitteeksi vain verkkotunnus");
            }
        }
        catch (System.IO.IOException ie)
        {
            Console.WriteLine(ie.Message);
            if (ie.Message.StartsWith("The operation is not allowed on non-connected sockets"))
            {
                Console.WriteLine("Soketti pitää yhdistää, ennen kuin se kiinnitetään NetworkStream -olioon");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            if (soketti != null)
            {
                // pyrkii varmistamaan että molemmat osapuolet
                // ehtivät lähettää kaiken datan
                if (soketti.Connected) soketti.Shutdown(SocketShutdown.Both);
                // suljetaan soketti ja vapautetaan resurssit
                soketti.Close();
                Console.WriteLine("Soketti suljettiin onnistuneesti");
            }
        }
    }
}


