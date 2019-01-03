using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Level : GameObject
{
    public Music levelMusic;

    const float GROUND_LAYER_SCALE = 0.20f;
    const float CAR_SCALE = 0.8f;
    const float BIKE_SCALE = 0.45f;
    const int ROAD_OFFSET = 2;
    //const int COLUMNS = 13;
    const int MAX_VICTIMS_ON_SCREEN = 10; //Don't change to 0
    const int MAX_CARS_ON_SCREEN = 3; //Do not change to 0
    const int MAX_BIKES_ON_SCREEN = 3; //Do not change to 0
    const int MAX_ENEMIES_ON_SCREEN = 1; //Don't change to 0
    const int ROADS = 2; //Ideally 2, just how the code works; Refer to CreateRoad(), SpawnerManager() and Road.cs!

    //Milliseconds
    const int ENEMY_SPAWN_DELAY = 6000; //6s
    const int VICTIM_SPAWN_DELAY = 2000; //2s
    const int CARS_SPAWN_DELAY = 6000; //6s
    const int BIKES_SPAWN_DELAY = 6000; //6s

    private Difficulty difficulty;

    //Timer alternative
    private int _lastUpdatedVictimTime;
    private int _lastUpdatedEnemyTime;
    private int _lastUpdatedCarsTime;
    private int _lastUpdatedBikesTime;
    private int _lastUpdatedRoadsTime;

    private float _scrollSpeed;
    private float _tileSize;

    private int _tileCount;

    private LevelLayer _floorLayer;
    private LevelLayer _groundPoopLayer;
    private LevelLayer _victimLayer;
    private LevelLayer _poopLayer;
    private LevelLayer _powerUpLayer;
    private LevelLayer _flightLayer;
    private LevelLayer _hudLayer;

    public Player player { get; private set; }

    private DeathManager deathManager;

    private Road _road;

    //Optimised checking for collision
    public List<Victim> victimList = new List<Victim> { };
    public List<Projectile> poopList = new List<Projectile> { };
    public List<Road> roadList = new List<Road> { };
    public List<Enemy> enemyList = new List<Enemy> { };
    public List<VictimCar> carsList = new List<VictimCar> { };
    public List<VictimBike> bikeList = new List<VictimBike> { };

    public Level() : base()
    {
        levelMusic = new Music();
        levelMusic.musicSoundChannel = levelMusic.gameTrack.Play();
        levelMusic.musicSoundChannel.Volume = Music.musicVolume;

        difficulty = new Difficulty(); //Don't ask please.

        //initialise variables
        _scrollSpeed = Difficulty.GetScrollSpeed();
        _tileSize = Difficulty.GetTileSize();
        _tileCount = 0;

        //Timer initialization
        _lastUpdatedVictimTime = 0;
        _lastUpdatedEnemyTime = 0;
        _lastUpdatedCarsTime = 0;
        _lastUpdatedBikesTime = 0;

        //Depth
        _floorLayer = new LevelLayer();
        _groundPoopLayer = new LevelLayer();
        _victimLayer = new LevelLayer();
        _poopLayer = new LevelLayer();
        _powerUpLayer = new LevelLayer();
        _flightLayer = new LevelLayer();
        _hudLayer = new LevelLayer();
        AddChild(_floorLayer);
        AddChild(_groundPoopLayer);
        AddChild(_victimLayer);
        AddChild(_poopLayer);
        AddChild(_powerUpLayer);
        AddChild(_flightLayer);
        AddChild(_hudLayer);

        deathManager = new DeathManager(this);
        AddChild(deathManager);
        
        CreatePlayer();
        CreateRoad();
        CreateHUD();
    }

    private void Update()
    {
        if (PauseManager.GetPause()) return;
        SpawnerManager();
    }

    private void CreatePlayer()
    {
        player = new Player(this);
        _flightLayer.AddChild(player);
        player.SetXY(MyGame.OldX() / 2, MyGame.OldY() / 2);
    }

    public void CreatePoop()
    {
        Projectile newPoop = new Projectile(this);
        _poopLayer.AddChild(newPoop);
        newPoop.SetXY(player.x, player.y);
        poopList.Add(newPoop);
    }

    public void CreateGroundPoop(float pX, float pY)
    {
        PoopGround newGroundPoop = new PoopGround();
        _groundPoopLayer.AddChild(newGroundPoop);
        newGroundPoop.SetXY(pX, pY);
    }

    private void CreateVictim()
    {
        string facing = (OurUtils.RandomBool()) ? "up" : "down";

        Victim newVictim = new Victim(this, facing);
        _victimLayer.AddChild(newVictim);
        newVictim.SetScaleXY(GROUND_LAYER_SCALE, GROUND_LAYER_SCALE);

        float positionModifier = MyGame.OldY() * ((facing == "up") ? 1 : -1);
        float positionX = 0;
        float positionY = Utils.Random(newVictim.height / 2, MyGame.OldY() - newVictim.height / 2) + positionModifier;

        positionX = newVictim.Boardwalk(positionX);

        newVictim.SetPositionVictim(newVictim, positionX, positionY);

        victimList.Add(newVictim);
    }



    private void CreateCar()
    {
        string facing = (OurUtils.RandomBool()) ? "up" : "down";

        VictimCar newCar = new VictimCar(this, facing);
        _victimLayer.AddChild(newCar);
        newCar.SetScaleXY(CAR_SCALE, CAR_SCALE);

        float positionModifier = MyGame.OldY() * ((facing == "up") ? 1 : -1);
        float positionX = 0;
        float positionY = Utils.Random(newCar.height / 2, MyGame.OldY() - newCar.height / 2) + positionModifier;

        positionX = newCar.CorrectLane(positionX, facing);

        newCar.SetPositionCar(newCar, positionX, positionY);

        carsList.Add(newCar);
    }

    private void CreateBike()
    {
        string facing = (OurUtils.RandomBool()) ? "up" : "down";

        VictimBike newBike = new VictimBike(this, facing);
        _victimLayer.AddChild(newBike);
        newBike.SetScaleXY(BIKE_SCALE, BIKE_SCALE);

        float positionModifier = MyGame.OldY() * ((facing == "up") ? 1 : -1);
        float positionX = 0;
        float positionY = Utils.Random(newBike.height / 2, MyGame.OldY() - newBike.height / 2) + positionModifier;

        positionX = newBike.CorrectLane(positionX, facing);

        newBike.SetPositionBike(newBike, positionX, positionY);

        bikeList.Add(newBike);
    }

    private void CreateEnemy()
    {
        Enemy newEnemy = new Enemy(this);
        _flightLayer.AddChild(newEnemy);
        newEnemy.SetXY(Utils.Random(newEnemy.width / 2, MyGame.OldX() - newEnemy.width / 2), Utils.Random(newEnemy.height / 2, MyGame.OldY() - newEnemy.height / 2) - MyGame.OldY());
        enemyList.Add(newEnemy);
    }

    private void CreateRoad(float spawnX = 0.0f, float spawnY = 0.0f, bool DownLeftOrigin = false)
    {
        Road newRoad = new Road(GetAssets.GetRandomRoad(), this);
        _floorLayer.AddChild(newRoad);
        if (DownLeftOrigin)
        {
            //Not used.
            newRoad.SetOrigin(0, newRoad.height);
        }
        newRoad.SetXY(spawnX, spawnY);
        roadList.Add(newRoad);
        _tileCount++;
        _lastUpdatedRoadsTime = Time.time;
    }

    private void CreateHUD()
    {
        HUD hud = new HUD(player);
        _hudLayer.AddChild(hud);
    }

    private void CreatePower<T>()
    {
        BasePower newPower;
        if (typeof(T) == typeof(Food))
        {
            newPower = new Food(this);
        }
        else if (typeof(T) == typeof(Laxative))
        {
            newPower = new Laxative(this);
        }
        else if (typeof(T) == typeof(SpeedUp))
        {
            newPower = new SpeedUp(this);
        }
        else if (typeof(T) == typeof(DoublePoints))
        {
            newPower = new DoublePoints(this);
        }
        else if (typeof(T) == typeof(BeakOfSteel))
        {
            newPower = new BeakOfSteel(this);
        }
        else if (typeof(T) == typeof(Constipation))
        {
            newPower = new Constipation(this);
        }
        else if (typeof(T) == typeof(RottenFood))
        {
            newPower = new RottenFood(this);
        }
        else
        {
            newPower = new Food(this);
        }
        _powerUpLayer.AddChild(newPower);
        newPower.SetXY(Utils.Random(100, MyGame.OldX() - 100), Utils.Random(newPower.height / 2, MyGame.OldY() - newPower.height / 2) - game.height * 2);
    }

    private void SpawnerManager()
    {
        if (enemyList.Count == MAX_ENEMIES_ON_SCREEN - 1)  
        {
            if (Time.time > ENEMY_SPAWN_DELAY + _lastUpdatedEnemyTime)
            {
                CreateEnemy();
                _lastUpdatedEnemyTime = Time.time;
            }
        }
        if (victimList.Count <= MAX_VICTIMS_ON_SCREEN)  
        {
            if (Time.time > VICTIM_SPAWN_DELAY + _lastUpdatedVictimTime)
            {
                CreateVictim();
                _lastUpdatedVictimTime = Time.time;
            }
        }
        if (carsList.Count <= MAX_CARS_ON_SCREEN)
        {
            if (Time.time > CARS_SPAWN_DELAY + _lastUpdatedCarsTime)
            {
                CreateCar();
                _lastUpdatedCarsTime = Time.time;
            }
        }
        if (bikeList.Count <= MAX_BIKES_ON_SCREEN)
        {
            if (Time.time > BIKES_SPAWN_DELAY + _lastUpdatedBikesTime)
            {
                CreateBike();
                _lastUpdatedBikesTime = Time.time;
            }
        }
        if (roadList[0] != null && roadList.Count >= 0)
        {
            _road = roadList[0] as Road;
            if (roadList.Count <= ROADS)
            {
                CreateRoad(0, roadList[roadList.Count - 1].y - _road.height + ROAD_OFFSET);
            }
        }
        if (_tileCount % 2 == 0 || _tileCount % 5 == 0 || _tileCount % 10 == 0)
        {
            if (_tileCount % 2 == 0)
            {
                IfTiles2();
            }
            if (_tileCount % 5 == 0)
            {
                IfTiles5();
            }
            if (_tileCount % 10 == 0)
            {
                IfTiles10();
            }
            _tileCount++;
        }

    }

	//private void CheckResize();
    private void IfTiles2()
    {
        
        int chance = Utils.Random(1, 101);
        if (chance > 0 && chance <= 15)
        {
            CreatePower<Laxative>();   
        }
        if (chance > 15 && chance <= 30)
        {
            CreatePower<SpeedUp>();
        }
        if (chance > 30 && chance <= 45)
        {
            CreatePower<DoublePoints>();
        }
        if (chance > 45 && chance <= 60)
        {
            CreatePower<BeakOfSteel>();
        }
        if (chance > 60 && chance <= 80)
        {
            CreatePower<Food>();
        }
        if (chance > 80 && chance <= 90)
        {
            CreatePower<Constipation>();
        }
        if (chance > 90 && chance <= 100)
        {
            CreatePower<RottenFood>();
        }
    }

    private void IfTiles5()
    {
        int chance = Utils.Random(1, 101);
        if (chance > 0 && chance <= 40)
        {
            CreatePower<Food>();
        }
        if (chance > 40 && chance <= 70)
        {
            CreatePower<Constipation>();
        }
        if (chance > 70 && chance <= 100)
        {
            CreatePower<RottenFood>();
        }
    }

    private void IfTiles10()
    {
        CreatePower<Food>();
    }

    protected override void OnDestroy()
    {
        levelMusic.musicSoundChannel.Stop();
        levelMusic.Destroy();
        /*
        foreach (GameObject killTarget in roadList)
        {
            killTarget.Destroy();
        }
        foreach (GameObject killTarget in poopList)
        {
            killTarget.Destroy();
        }
        foreach (GameObject killTarget in victimList)
        {
            killTarget.Destroy();
        }
        foreach (GameObject killTarget in carsList)
        {
            killTarget.Destroy();
        }
        foreach (GameObject killTarget in bikeList)
        {
            killTarget.Destroy();
        }
        foreach (GameObject killTarget in enemyList)
        {
            killTarget.Destroy();
        } */
        _road.Destroy();
        player.Destroy();
        deathManager.Destroy();
        difficulty.Destroy();
        _flightLayer.Destroy();
        _floorLayer.Destroy();
        _groundPoopLayer.Destroy();
        _hudLayer.Destroy();
        _poopLayer.Destroy();
        _powerUpLayer.Destroy();
        _victimLayer.Destroy();
    }
}
