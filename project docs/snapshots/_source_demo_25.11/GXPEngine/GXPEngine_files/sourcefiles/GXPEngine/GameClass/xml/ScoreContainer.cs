using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("highscores")]
public class HighScores
{
    [XmlElement("highscore")]
    public List<HighScore> highScore = new List<HighScore> { };

    public HighScores()
    {

    }
}

[XmlRoot("highscore")]
public class HighScore
{
    [XmlAttribute("position")]
    public int position = 0;

    [XmlAttribute("name")]
    public string name;

    [XmlAttribute("score")]
    public double score = 0;

    public HighScore()
    {

    }
}
