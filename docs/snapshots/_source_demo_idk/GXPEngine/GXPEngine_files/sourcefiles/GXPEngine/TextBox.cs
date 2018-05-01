using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using GXPEngine;

public class TextBox : Canvas
{
    private float _positionX;
    private float _positionY;
    
    private bool _isDestroyed;
    private Font _hudFont;
    private PrivateFontCollection _fontCollection;

    public TextBox(int pWidth, int pHeight, float pX, float pY) : base(pWidth, pHeight)
    {
        SetText("");
        _isDestroyed = false;

        _positionX = pX;
        _positionY = pY;

        _fontCollection = new PrivateFontCollection();
        _fontCollection.AddFontFile("assets/fonts/Dream MMA.ttf");

        _hudFont = new Font(_fontCollection.Families[0], 21);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _isDestroyed = true;
    }

    public void SetText(string text)
    {
        if (_isDestroyed == false)
        {
            graphics.Clear(Color.Transparent);
            graphics.DrawString(text, _hudFont, Brushes.PowderBlue, _positionX, _positionY);
            alpha = 0.9f;
        }
    }
    public void SetText(string text, float pX, float pY)
    {
        if (_isDestroyed == false)
        {
            graphics.Clear(Color.Transparent);
            graphics.DrawString(text, _hudFont, Brushes.PowderBlue, pX, pY);
            alpha = 0.9f;
        }
    }
}