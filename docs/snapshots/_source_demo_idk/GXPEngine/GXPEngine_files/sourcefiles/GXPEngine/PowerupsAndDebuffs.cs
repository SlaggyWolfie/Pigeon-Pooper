using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public abstract class BasePower : Sprite
{
    protected float offset;
    private bool gotCoords;
    protected float startingX;
    protected float startingY;
    protected float speed;
    protected float scaleSprites;

    protected Level level;

    public BasePower(string filename, Level pLevel) : base(filename)
    {
        scaleSprites = 0.125f;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(scaleSprites, scaleSprites);
        gotCoords = false;

        level = pLevel;
    }

    protected virtual void Update()
    {
        GetStartingCoordinates();

        y += Difficulty.GetScrollSpeed();

        GotOutOfTheWindow();
    }

    private void GetStartingCoordinates()
    {
        if (!gotCoords)
        {
            startingX = x;
            startingY = y;
            gotCoords = true;
        }
    }

    private void GotOutOfTheWindow()
    {
        if (y >= game.height + height)
        {
            Destroy();
        }
    }
}

public abstract class BasePowerup : BasePower
{
    protected float pulse;
    protected float startingPulse;

    public BasePowerup(string filename, Level pLevel) : base(filename, pLevel)
    {
        offset = 0.08f * scaleSprites;
        speed = 0.01f * scaleSprites;
        startingPulse = 1f * scaleSprites;
        pulse = startingPulse;
    }

    protected override void Update()
    {
        base.Update();
        if (HitTest(level.player))
        {
            level.player.score += 50;
        }
        startingY = y;
        Pulse();
    }

    private void Pulse()
    {
        pulse += speed;
        SetScaleXY(pulse, pulse);
        if (pulse >= startingPulse + offset) speed = -speed;
        if (pulse <= startingPulse - offset) speed = -speed;
    }

    private void FloatInTheAir()
    {
        //Not used currently
        y += speed;
        if (y >= startingY + offset) speed = -speed;
        if (y <= startingY - offset) speed = -speed;
    }
}

public abstract class BaseDebuff : BasePower
{

    public BaseDebuff(string filename, Level pLevel) : base(filename, pLevel)
    {
        SetOrigin(width / 2, height / 2);
        speed = 1f;
        //SetColor(0.05f, 0.05f, 0.05f);
    }

    protected override void Update()
    {
        base.Update();
        Triggered();
    }
    

    private void Triggered() //Not really triggered.
    {
        x += speed;
        if (x >= startingX + offset) speed = -speed;
        if (x <= startingX - offset) speed = -speed;
    }
}

public class Food : BasePowerup
{
    private int _foodEnergy;
    public Food(Level pLevel) : base("assets/powerups_debuffs/Pizza.png", pLevel)
    {
        _foodEnergy = 25;
    }

    protected override void Update()
    {
        base.Update();
        if (HitTest(level.player))
        {
            Destroy();
            level.player.energy += _foodEnergy;
        }
    }
}
public class Laxative : BasePowerup
{
    private int _laxativeLength;
    public Laxative(Level pLevel) : base("assets/powerups_debuffs/Diarrhea.png", pLevel)
    {
        _laxativeLength = 10000; //10s
    }

    protected override void Update()
    {
        base.Update();
        if (HitTest(level.player))
        {
            Destroy();
            level.player.poopCost = 0;
            Timer laxativeLength = new Timer(_laxativeLength, Reset);
        }
    }

    private void Reset()
    {
        level.player.poopCost = level.player.startingPoopCost;
    }
}

public class SpeedUp : BasePowerup
{
    private int _speedUpLength;
    private int _speedUp;
    public SpeedUp(Level pLevel) : base("assets/powerups_debuffs/Energy.png", pLevel)
    {
        _speedUpLength = 10000; //10s
        _speedUp = 2;
    }

    protected override void Update()
    {
        base.Update();
        if (HitTest(level.player))
        {
            Destroy();
            level.player.speed += _speedUp;
            Timer laxativeLength = new Timer(_speedUpLength, Reset);
        }
    }

    private void Reset()
    {
        level.player.speed -= _speedUp;
    }
}

public class DoublePoints : BasePowerup
{
    private int _doublePointsLength;
    public DoublePoints(Level pLevel) : base("assets/powerups_debuffs/DoublePoints.png", pLevel)
    {
        _doublePointsLength = 10000; //10s
    }

    protected override void Update()
    {
        base.Update();
        if (HitTest(level.player))
        {
            Destroy();
            Difficulty.SetScoreModifier(Difficulty.GetScoreModifier() * 2);
            Timer doublePointsLength = new Timer(_doublePointsLength, Reset);
        }
    }

    private void Reset()
    {
        Difficulty.SetScoreModifier(Difficulty.GetScoreModifier() / 2);
    }
}

public class BeakOfSteel : BasePowerup
{
    private int _beakOfSteelLength;
    public BeakOfSteel(Level pLevel) : base("assets/powerups_debuffs/BeakOfSteel.png", pLevel)
    {
        _beakOfSteelLength = 10000; //10s
    }

    protected override void Update()
    {
        base.Update();
        if (HitTest(level.player))
        {
            Destroy();
            level.player.canKill = true;
            Timer beakOfSteelLength = new Timer(_beakOfSteelLength, Reset);
        }
    }

    private void Reset()
    {
        level.player.canKill = false;
    }
}

public class Constipation : BaseDebuff
{
    private int _constipationLength;
    public Constipation(Level pLevel) : base("assets/powerups_debuffs/Cork.png", pLevel)
    {
        _constipationLength = 5000; //5s
    }

    protected override void Update()
    {
        base.Update();
        if (HitTest(level.player))
        {
            Destroy();
            level.player.canPoop = false;
            Timer constipationLength = new Timer(_constipationLength, Reset);
        }
    }

    private void Reset()
    {
        level.player.canPoop = true;
    }
}

public class RottenFood : BaseDebuff
{
    private int _foodEnergyLoss;
    public RottenFood(Level pLevel) : base("assets/powerups_debuffs/RottenPizza.png", pLevel)
    {
        _foodEnergyLoss = 25;
    }

    protected override void Update()
    {
        base.Update();
        if (HitTest(level.player))
        {
            Destroy();
            level.player.energy -= _foodEnergyLoss;
        }
    }
}

public class SpeedDown : BaseDebuff
{
    private int _speedDownLength;
    private int _speedDown;
    public SpeedDown(Level pLevel) : base("assets/powerups_debuffs/RottenPizza.png", pLevel)
    {
        _speedDownLength = 10000; //10s
        _speedDown = 2;
    }

    protected override void Update()
    {
        base.Update();
        if (HitTest(level.player))
        {
            Destroy();
            level.player.speed -= _speedDown;
            Timer speedDownLength = new Timer(_speedDownLength, Reset);
        }
    }

    private void Reset()
    {
        level.player.speed += _speedDown;
    }
}
