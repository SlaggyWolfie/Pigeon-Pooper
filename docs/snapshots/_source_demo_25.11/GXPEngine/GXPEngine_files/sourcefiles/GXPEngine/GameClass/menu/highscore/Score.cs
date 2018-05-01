using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GXPEngine;


public class Score : Sprite
{
    private TextBox scores;
    private HighScores hs;

    //private List<HighScore> highScoresList = new List<HighScore> { };
    public Score() : base("assets/menu/bgmenu.png")
    {
        //ScoreParser hsParser = new ScoreParser();
        //HighScores highScore = hsParser.Parse("assets/menu/highscores.xml");

        //string name = highScore.name;
        //double score = highScore.score;
        //int position = highScore.position;


        //scoreScreen = new Sprite("assets/menu/bgmenu.png");
        //AddChild(scoreScreen);
        SetScaleXY(MyGame.OldX() / width, MyGame.OldY() / height);
        scores = new TextBox(game.width, game.height, 0, 0);
        ScoreParser sp = new ScoreParser();
        hs = sp.Parse("assets/menu/highscores.xml");
        AddChild(scores);
    }
    public void GetScore()
    {
        TextBox titleBox = new TextBox(game.width, game.height, 0, 0);
        AddChild(titleBox);
        titleBox.SetText("top highscores", MyGame.OldX() / 2, MyGame.OldY() / 8);
        
        for (int i = 0; i < Math.Min(hs.highScore.Count, 5); i++)
        {
            Console.WriteLine(i);
            TextBox scoreBox = new TextBox(game.width, game.height, 0, 0, 16);
            AddChild(scoreBox);
            string text = hs.highScore[i].position + ". " + hs.highScore[i].name + ": " + hs.highScore[i].score;
            Console.WriteLine(text);
            scoreBox.SetText(text, MyGame.OldX() / 4, MyGame.OldY() / 3 + i * 25);

        }
    }



    public void SetScore(string name, double score)
    {
        HighScore newHighScore = new HighScore();

        newHighScore.name = name;
        newHighScore.score = score;
        newHighScore.position = hs.highScore.Count + 1;

        hs.highScore.Add(newHighScore);

        hs.highScore.Sort((a, b) => b.score.CompareTo(a.score));

        for (int i = 0; i < hs.highScore.Count; i++)
        {
            hs.highScore[i].position = i + 1;
        }

        WriteScore();
    }

    public void WriteScore()
    {
        using (XmlWriter writer = XmlWriter.Create("assets/menu/highscores.xml"))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("highscores");
            
            foreach (HighScore scores in hs.highScore)
            {
                writer.WriteStartElement("highscore");

                writer.WriteAttributeString("score", scores.score.ToString());
                writer.WriteAttributeString("name", scores.name);
                writer.WriteAttributeString("position", scores.position.ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }
}