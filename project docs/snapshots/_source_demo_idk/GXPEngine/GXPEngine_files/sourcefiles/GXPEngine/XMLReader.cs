using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("highscores")]
public class Highscores
{
    [XmlElement("playerscore")]
    public PlayerScore[] playerScore;
}

[XmlRoot("playerscore")]
public class PlayerScore
{
    [XmlAttribute("position")]
    public int position = 0;

    [XmlAttribute("name")]
    public string name;

    [XmlAttribute("score")]
    public double score = 0;
}