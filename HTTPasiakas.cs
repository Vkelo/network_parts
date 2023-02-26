using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
public class httpAsiakasLuokka
{
    public static void Main()
    {
#pragma warning disable 108, SYSLIB0014
        double pisteet = 4.0;
        if (pisteet == 4.0)
        {
            string url = "www.example.com";
            Console.WriteLine("Esimerkki URL: {0} palauttaa:\r\n", url);
            System.Console.WriteLine("-----------------------------------------------------------------");
            string sivu = httpAsiakas(url);
            Console.WriteLine(sivu);
            System.Console.WriteLine("-----------------------------------------------------------------");
        }
    }

    // BYCODEBEGIN
    /// <summary>
    /// Funktio palauttaa HTML sivun.
    /// </summary>
    /// <param name="url">URL tekstimuotoiselle HTML-sivulle</param>
    /// <returns>string HTML-sivu tekstinä</returns>
    public static string httpAsiakas(string url)
    {
        if (url.StartsWith('w')) url = "http://" + url;
        if (url.StartsWith('u')) url = "http://" + url;
        Uri myUri = new Uri(url);
        string hostpart = myUri.Authority;
        string pathpart = myUri.PathAndQuery;
        if (pathpart == "" || pathpart.Equals("")) pathpart = "/";

        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        string sivu = hostpart;
        string resurssi = pathpart;

        s.Connect(sivu, 80);

        string snd = "GET " + resurssi + " HTTP/1.1\r\nHost:" + sivu + "\r\n\r\n";
        byte[] buffer = Encoding.UTF8.GetBytes(snd);
        s.Send(buffer);

        byte[] rec = new byte[4096];
        int paljon = s.Receive(rec);
        String vastaus = System.Text.Encoding.ASCII.GetString(rec, 0, paljon);

        s.Close();
        int index = vastaus.IndexOf("\r\n\r\n") + 4;
        string sivu2 = vastaus.Substring(index);
        return sivu2;
    }
    // BYCODEEND
}


