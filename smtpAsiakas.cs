using System;
public class smtpAsiakasLuokka
{
    public static void Main()
    {
        try
        {
        }
        catch (IndexOutOfRangeException iore)
        {
            Console.WriteLine(iore.Message);
            Console.WriteLine("Todennäköisesti (1) lähettämästäsi viestistä puuttuu välilyönti tai sen jälkeinen osa.");
            Console.WriteLine("tai (2) palvelimen lähettämää viestiä on pilkottu väärin.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    // BYCODEBEGIN
    /// <summary>
    /// Funktio palauttaa SMTP asiakkaan viestin palvelimen viestin perusteella.
    /// </summary>
    /// <param name="viesti">string SMTP palvelimen viesti</param>
    /// <returns>string SMTP asiakkaan viesti</returns>
    public static string smtpAsiakas(string vastaanotettu)
    {
        string lahetettava = "QUIT";
        Console.Write(vastaanotettu);
        string[] status = vastaanotettu.Split(' ');

        if (status[0] == "220") lahetettava = "HELO jyu.fi";
        if (status[0] == "354") lahetettava = "sen email\r\n.";
        if (status[0] == "221") lahetettava = "";

        if (status[0] == "250") 
        {
            if (status[1] == "ITKP104") lahetettava = "MAIL FROM: mie"; 
            if (status[1] == "2.1.0") lahetettava = "RCPT TO: sie";
            if (status[1] == "2.1.5") lahetettava = "DATA";
            if (status[1] == "2.0.0") lahetettava = "QUIT";
        }

        Console.Write(lahetettava);
        return lahetettava + "\r\n";
    }
    // BYCODEEND
}

