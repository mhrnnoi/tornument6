using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
public enum Func
{
    Win,
    Draw,
    Loss

}
public class Team
{
    public int MathPlayed { get; set; }
    public int Wins { get; set; }
    public int Loss { get; set; }
    public int Draw { get; set; }
    public int Points { get; set; }
    public string Name { get; set; }

    public Team(string name)
    {
        Name = name;
        Wins = 0;
        Loss = 0;
        Draw = 0;
        Points = 0;
        MathPlayed = 0;
    }

    public void add(Func fun)
    {
        switch (fun)
        {
            case Func.Win:
                this.Wins++;
                this.MathPlayed++;

                break;
            case Func.Loss:
                this.Loss++;
                this.MathPlayed++;

                break;

            case Func.Draw:
                this.Draw++;
                this.MathPlayed++;

                break;





            default:
                throw new InvalidCastException();
        }
    }
    public void Calculate()
    {
        var sum = 0;
        sum += (this.Wins * 3);
        sum += this.Draw;
        this.Points = sum;

    }


}
public static class Tournament
{
    public static void Tally(Stream inStream, Stream outStream)
    {

        if (0 == inStream.Length)
        {
            StreamWriter writer = new StreamWriter(outStream);
            writer.Write("Team                           | MP |  W |  D |  L |  P");
            writer.Dispose();
            writer.Close();


        }
        else
        {
            try
            {
                StreamReader reder = new StreamReader(inStream);
                string? g;
                string[] r;
                var lst = new List<string>();
                var lst2 = new List<Team>();


                while (!(reder.EndOfStream))
                {
                    g = reder.ReadLine();


                    if (!string.IsNullOrWhiteSpace(g))
                    {
                        r = g.Split(";");
                        switch (r[2])
                        {
                            case "win":
                                lst2.First(a => a.Name == r[0]).add(Func.Win);
                                lst2.First(a => a.Name == r[1]).add(Func.Loss);
                                break;

                            case "draw":
                                lst2.First(a => a.Name == r[0]).add(Func.Draw);
                                lst2.First(a => a.Name == r[1]).add(Func.Draw);
                                break;
                            case "loss":
                                lst2.First(a => a.Name == r[0]).add(Func.Loss);
                                lst2.First(a => a.Name == r[1]).add(Func.Win);
                                break;

                            default:
                                throw new InvalidOperationException();
                        }

                    }




                }
                inStream.Dispose();
                inStream.Close();
                reder.Dispose();
                reder.Close();

                foreach (var item in lst2)
                {
                    item.Calculate();
                }
                var gg = lst2.OrderBy(a => a.Name);
                gg = gg.OrderByDescending(a => a.Points);

                List<string> jdvl = new List<string>();
                jdvl.Add("Team                           | MP |  W |  D |  L |  P");
                string fsle = "";
                foreach (var item in gg)
                {
                    for (int i = 0; i < 31 - item.Name.Length; i++)
                    {
                        fsle += " ";
                    }
                    var fslep = 2;
                    if (item.Points > 9)
                    {
                        fslep = 1;
                    }
                    var st = "";
                    for (int i = 0; i < fslep; i++)
                    {
                        st += " ";
                    }
                    jdvl.Add($"{item.Name}{fsle}|  {item.MathPlayed} |  {item.Wins} |  {item.Draw} |  {item.Loss} |{st}{item.Points}");
                    fsle = "";

                }
                StreamWriter writer = new StreamWriter(outStream);

                foreach (var item in jdvl)
                {
                    if (jdvl.Last() == item)
                    {
                        writer.Write(item);

                    }
                    else
                    {
                        writer.WriteLine(item);

                    }

                }
                writer.Dispose();
                writer.Close();



            }
            catch (System.Exception)
            {

                throw;
            }
            finally
            {

            }
        }


    }
}
