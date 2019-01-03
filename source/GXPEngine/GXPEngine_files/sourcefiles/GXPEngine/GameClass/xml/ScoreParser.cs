using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

public class ScoreParser
{
    public ScoreParser()
    {

    }

    public HighScores Parse(string filename)
    {
        XmlSerializer xs = new XmlSerializer(typeof(HighScores));

        TextReader tr = new StreamReader(filename);
        HighScores hs = xs.Deserialize(tr) as HighScores;
        tr.Close();

        return hs;
    }
}
