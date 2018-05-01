using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Level : GameObject
{
    const float GROUND_LAYER_SCALE = 0.25f;
    const int ROAD_OFFSET = 5;
    //const int COLUMNS = 13;
    const int MAX_VICTIMS_ON_SCREEN = 10; //Don't change to 0
    const int MAX_ENEMIES_ON_SCREEN = 1; //Don't change to 0
    const int ROADS = 2; //Ideally 2, just how the code works; Refer to CreateRoad(), SpawnerManager() and Road.cs!

    //Milliseconds
    const int ENEMY_SPAWN_DELAY = 6000; //6s
    const int VICTIM_SPAWN_DELAY = 2000; //2s

    private Difficulty difficulty;

    //Timer alternative
    private int _lastUpdatedVictimTime;
    private int _lastUpdatedEnemyTime;

    private float _scrollSpeed;
    private float _tileSize;

    private LevelLayer _floorLayer;
    private LevelLayer _victimLayer;
    public LevelLayer _poopLayer;
    private LevelLayer _flightLayer;
    private LevelLayer _hudLayer;

    public Player player { get; private set; }
    private Road _road;

    //Optimised checking for collision
    public List<Victim> victimList = new List<Victim> { };
    public List<Projectile> poopList = new List<Projectile> { };
    public List<Road> roadList = new List<Road> { };
    public List<string> differentRoads = new List<string> {"road3.1_-_no_lights.png", "road3.1_-_lights.png"};
    public List<GameObject> wallList = new List<GameObject> { };
    public List<Enemy> enemyList = new List<Enemy> { };

    public Level() : base()
    {
        difficulty = new Difficulty(); //Don't ask please.

        //initialise variables
        _scrollSpeed = Difficulty.GetScrollSpeed();
        _tileSize = Difficulty.GetTileSize();

        //Timer initialization
        _lastUpdatedVictimTime = 0;
        _lastUpdatedEnemyTime = 0;

        //Depth
        _floorLayer = new LevelLayer();
        _victimLayer = new LevelLayer();
        _poopLayer = new LevelLayer();
        _flightLayer = new LevelLayer();
        _hudLayer = new LevelLayer();
        AddChild(_floorLayer);
        AddChild(_victimLayer);
        AddChild(_poopLayer);
        AddChild(_flightLayer);
        AddChild(_hudLayer);
        
        CreatePlayer();
        CreateRoad();
        //CreateEnemy();
        CreateHUD();
    }

    private void Update()
    {
        SpawnerManager();
    }

    private void CreatePlayer()
    {
        player = new Player(this);
        _flightLayer.AddChild(player);
        player.SetXY(game.width / 2, game.height / 2);
    }

    public void CreatePoop()
    {
        Projectile newPoop = new Projectile(this);
        _poopLayer.AddChild(newPoop);
        newPoop.SetXY(player.x, player.y);
        poopList.Add(newPoop);
    }

    private void CreateVictim()
    {
        Victim newVictim = new Victim(this);
        _victimLayer.AddChild(newVictim);
        newVictim.SetScaleXY(GROUND_LAYER_SCALE, GROUND_LAYER_SCALE);
        newVictim.SetXY(Utils.Random(newVictim.width * 2, game.width - newVictim.width * 2), Utils.Random(newVictim.height / 2, game.height - newVictim.height / 2) - game.height);
        victimList.Add(newVictim);
    }

    private void CreateEnemy()
    {
        Enemy newEnemy = new Enemy(this);
        _flightLayer.AddChild(newEnemy);
        newEnemy.SetXY(Utils.Random(newEnemy.width / 2, game.width - newEnemy.width / 2), Utils.Random(newEnemy.height / 2, game.height - newEnemy.height / 2) - game.height);
        enemyList.Add(newEnemy);
        
    }

    private void CreateWall()
    {
        Wall newWall = new Wall(this);
        _flightLayer.AddChild(newWall);
        wallList.Add(newWall);
        if (wallList.Count < 3)
        {
            Timer wallSpawner = new Timer(1500, CreateWall);
        }
    }

    private void CreateRoad(float spawnX = 0.0f, float spawnY = 0.0f)
    {
        Road newRoad = new Road(RandomRoadTile(), this);
        _floorLayer.AddChild(newRoad);
        newRoad.SetXY(spawnX, spawnY + ROAD_OFFSET);
        roadList.Add(newRoad);
    }

    private void CreateHUD()
    {
        HUD hud = new HUD(player);
        _hudLayer.AddChild(hud);
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
        if (roadList[0] != null && roadList.Count >= 0)
        {
            _road = roadList[0] as Road;
                if (_road.y > 0 && roadList.Count <= ROADS)
                {
                    CreateRoad(0, -_road.height);
                }
        } 
    }

    private string RandomRoadTile()
    {
        string roadTile = differentRoads[Utils.Random(0, differentRoads.Count)] as string;
        return roadTile;
    }

    private void CheckResize()
    {
        //not used yet
        if (MyGame.OldX() != game.width || MyGame.OldY() != game.height)
        {

        }
    }

    private void UpwardsScroll()
    {
        //Not used currently.
        y += _scrollSpeed;
    }

    private void CommentedOutCode()
    {
        
        /*
        Road firstRoad = new Road(this);
        AddChild(firstRoad);
        firstRoad.SetScaleXY(GROUND_STUFF_SCALE, GROUND_STUFF_SCALE);
        firstRoad.SetXY(game.width / 4, game.height - _tileSize);
        roadList.Add(firstRoad); */
        //newRoad.y += _scrollSpeed;
        /*
        if (roadList.Count > 0)
        {
            if (roadList.Count < COLUMNS * _row)
            { 
                _road = roadList[roadList.Count - 1] as Road;
                if (!_road.HitTestPoint(_road.x + _tileSize, _road.y))
                {
                    Road newRoad = new Road(this);
                    _floorLayer.AddChild(newRoad);
                    newRoad.SetScaleXY(GROUND_STUFF_SCALE, GROUND_STUFF_SCALE);
                    newRoad.SetXY(_road.x + _tileSize * GROUND_STUFF_SCALE, _road.y);
                    roadList.Add(newRoad);
                }
            }
            if (roadList.Count == COLUMNS * _row)
            {
                _road = roadList[COLUMNS * (_row - 1)] as Road;
                if (!_road.HitTestPoint(_road.x, _road.y - _tileSize))
                {
                    Road newRoad = new Road(this);
                    _floorLayer.AddChild(newRoad);
                    newRoad.SetScaleXY(GROUND_STUFF_SCALE, GROUND_STUFF_SCALE);
                    newRoad.SetXY(_road.x, _road.y - _tileSize * GROUND_STUFF_SCALE);
                    roadList.Add(newRoad);
                    _row++;
                }
            }
        } */
        /*
        foreach (Road road in roadList)
        {
            if (!road.HitTestPoint(road.x + _tileSize, road.y))
            {
                Road newRoad = new Road(this);
                AddChild(newRoad);
                newRoad.SetScaleXY(GROUND_STUFF_SCALE, GROUND_STUFF_SCALE);
                newRoad.SetXY(road.x + _tileSize * GROUND_STUFF_SCALE, road.y);
                roadList.Add(newRoad);
            }
        } */
        //for (int i = 0; )
        /*
        foreach (Road road in roadList)
        {
            Victim pro = new Victim(this);
            AddChild(pro);
            pro.SetScaleXY(GROUND_STUFF_SCALE, GROUND_STUFF_SCALE);
            pro.SetXY(_road.x + _tileSize * GROUND_STUFF_SCALE, _road.y);
            //roadList.Add(pro);
        } */
    }
}
