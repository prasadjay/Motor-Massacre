using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace myEp3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Camera camera { get; protected set; }
        GraphicsDevice device;

        int vehX = 0;
        int vehY = 1;
        int vehZ = 0;

        #region GAME_MENUS

        #region handle_MOUSE

        Texture2D mouse;
        MouseState mouseState = new MouseState();
        MouseState prevMouseState;
        Rectangle mouseRect;
        Vector2 mousePos = Vector2.Zero;

        #endregion


        #region MainMenu

        GameSprite LogoScreen;
        GameSprite NameScreen;
        GameSprite[] numbers = new GameSprite[12];
        GameSprite[] alphabet = new GameSprite[26];

        GameSprite MenuBackground_1024;
        GameSprite btn_ServerStarted;
        GameSprite btn_StartServer;
        GameSprite btn_JoinGame;
        GameSprite btn_Profile;
        GameSprite btn_Customize;
        GameSprite btn_Settings;
        GameSprite btn_Help;
        GameSprite btn_Credits;
        GameSprite btn_Quit;

        #endregion

        #region CreateServer

        GameSprite CreateServerBackground_1024;
        GameSprite btn_LaunchServer;
        GameSprite img_map1;
        GameSprite img_map2;
        GameSprite img_map3;
        GameSprite btn_map1;
        GameSprite btn_Sel_map1;
        GameSprite btn_map2;
        GameSprite btn_Sel_map2;
        GameSprite btn_map3;
        GameSprite btn_Sel_map3;
        GameSprite noOfPlayers;

        bool map1_bool = false;
        bool map2_bool = false;
        bool map3_bool = false;

        bool[] playerCountState = new bool[8];
        int playerCount = 0;

        #endregion

        #region JoinGame

        GameSprite JoinServerBackground_1024;
        GameSprite enterIP;
        GameSprite btn_JoinServer;
        GameSprite WaitForServerBackground_1024;
        string joinIP = "";


        #endregion

        #region Profile

        GameSprite ProfileBackground_1024;
        GameSprite ProfileNameChange_1024;
        GameSprite[] Slot = new GameSprite[3];
        GameSprite[] btn_Edit = new GameSprite[3];
        GameSprite btn_Save;

        int selectedProfile=1;
        bool NameEditMode = false;

        string[] users;
        string[] profileNames = new string[3];
        string[] carNames = new string[3];
        string[] myCars = new string[3];

        string typedName = "SAMPLE";

        

        #endregion

        #region CARS_WHEELS_PAINTS

        string[] vehicles = new string[10];

       

        #endregion


        #region Customization

        GameSprite CustomizeBackground;
        GameSprite[] carChoice = new GameSprite[10];
        GameSprite[] carInfo = new GameSprite[10];
        GameSprite[] carChoiceSelected = new GameSprite[10];
        GameSprite confirmCarButton;
        GameSprite confirmCarSelectedButton;
        string confirmedCar = "0";
        bool confimMouseOver = false;
        

        #endregion

        #region Settings

        GameSprite Background_Settings;
        GameSprite AudioButton;
        GameSprite VideoButton;
        GameSprite ControlsButton;
        GameSprite AudioButtonSelected;
        GameSprite VideoButtonSelected;
        GameSprite ControlsButtonSelected;
        bool[] SelectedSetting= new bool[3];

        #endregion

        #region Controls

        GameSprite Background_Controls;
        GameSprite option1;
        GameSprite option2;
        GameSprite option1_Sel;
        GameSprite option2_Sel;
        bool[] selectedControl = new bool[2];
        int Control = 1;

        #endregion

        #region Audio_Settings

        GameSprite Background_Audio;

        GameSprite BackMusic;
        GameSprite EngineNoise;
        GameSprite EnvSounds;

        GameSprite BackMusicON;
        GameSprite BackMusicON_Sel;
        GameSprite EngineNoiseON;
        GameSprite EngineNoiseON_Sel;
        GameSprite EnvSoundON;
        GameSprite EnvSoundON_Sel;

        GameSprite BackMusicOFF;
        GameSprite BackMusicOFF_Sel;
        GameSprite EngineNoiseOFF;
        GameSprite EngineNoiseOFF_Sel;
        GameSprite EnvSoundOFF;
        GameSprite EnvSoundOFF_Sel;

        bool[] audioStat = new bool[3];
        

        #endregion

        #region Credits

        GameSprite CreditsScreen;

        #endregion

        #region Video_Settings


        GameSprite Background_Video;
        GameSprite Background_Video_Res;

        GameSprite PostProcessing;
        GameSprite ParticleSystem;
        GameSprite MotionBlur;
        GameSprite Resolution;

        GameSprite PostProcessingON;
        GameSprite PostProcessingON_Sel;
        GameSprite ParticleSystemON;
        GameSprite ParticleSystemON_Sel;
        GameSprite MotionBlurON;
        GameSprite MotionBlurON_Sel;

        GameSprite PostProcessingOFF;
        GameSprite PostProcessingOFF_Sel;
        GameSprite ParticleSystemOFF;
        GameSprite ParticleSystemOFF_Sel;
        GameSprite MotionBlurOFF;
        GameSprite MotionBlurOFF_Sel;

        GameSprite Res768;
        GameSprite Res720;
        GameSprite Res1080;
        GameSprite Res1440;
        GameSprite Res768_Sel;
        GameSprite Res720_Sel;
        GameSprite Res1080_Sel;
        GameSprite Res1440_Sel;
        bool[] SelectedResolutionTab = new bool[4];

        bool ResEditMode = false;
        bool[] videoStat = new bool[3];

        Int32 width;
        Int32 height;

        #endregion


        #endregion

        #region For_All_Maps

        int health = 100;
        float meterSpeed = 0;
        float meterRpm = 1500;
        GameSprite overlayFrame;
        SpriteFont speedMeter;
        GameSprite[] speedoMeters = new GameSprite[3];
        GameSprite crosshair;

        #endregion

        #region MAP_1

        //Lasitha write your codes here to create variables for Map 1 

        SimpleModel mud1;
        SimpleModel road1;

        SimpleModel bcar1;
        SimpleModel bcar2;
        SimpleModel bcar3;
        SimpleModel bcar4;
        SimpleModel bcar5;

        SimpleModel[] wall_right = new SimpleModel[13];
        SimpleModel[] wall_left = new SimpleModel[13];
        SimpleModel[] wall_up = new SimpleModel[13];
        SimpleModel[] wall_down = new SimpleModel[13];

        Vehicle vr;
        Tyre tr1;
        Tyre tr2;

        Tyre tl1;
        Tyre tl2;
        //junk tyre sets

        SimpleModel tire_in1;
        SimpleModel tire_in2;
        SimpleModel tire_in3;

        SimpleModel tire_out1;
        SimpleModel tire_out2;
        SimpleModel tire_out3;
        SimpleModel tire_out4;


        SimpleModel[] panes = new SimpleModel[4];

        #endregion

        #region Map2

        SimpleModel[] FloorMap2 = new SimpleModel[25];
        SimpleModel[] WallMap2 = new SimpleModel[64];
        SimpleModel[] TreeMap2 = new SimpleModel[5];
        SimpleModel[] PaneMap2 = new SimpleModel[4];
        SimpleModel[] BrokenCarMap2 = new SimpleModel[3];
        SimpleModel[] JunkTireMap2 = new SimpleModel[4];

        #endregion

        enum ScreenState
        {
            Logo = 1,
            GameName = 2,
            MainMenu = 3,
            CreateServer = 4,
            JoinGame = 5,
            WaitToJoin = 6,
            Lobby = 7,
            Profile = 8,
            Customize = 9,
            SettingsMains = 10,
            SettingsAudio = 11,
            SettingsVideo = 12,
            SettingsControls = 13,
            Credits = 14,
            Map1,
            Map2
        }


        
        ScreenState currentState;

        

        KeyboardState lastKeyboardState = new KeyboardState();
        KeyboardState currentKeyboardState = new KeyboardState();



        #region Particles_PostProcessing

        //Explosions
        List<ParticleExplosion> explosions = new List<ParticleExplosion>(); 
        ParticleExplosionSettings partcleExpSettings = new ParticleExplosionSettings();
        ParticleSettings particleSettings = new ParticleSettings();
        Texture2D explotionTexture;
        Texture2D explosionColorsTexture;
        Effect explosionEffect;

        //Lighting Effects

        BloomComponent bloom;
        int bloomSettingsIndex = 0;

        

        #endregion

        Random rnd = new Random();

        SpriteFont myFont;
        SpriteFont IPAddress;

        const string ff = "sampleee.txt";
        StreamReader net = new StreamReader(ff);


        #region Networking

        MyClient client;
        MyServer server;

        bool serverStarted = false;
        bool clientStarted = false;


        #endregion


        #region Audio

        Audio BackgroundMusic;
        Audio MenuItemSelect;
        Audio LogoMusic;
        Audio GameNameMusic;
        Audio Brake;
        Audio MapBackground;
        Audio ChallengerIdle;
        Audio ChallengerRace;
        Audio Fire;



        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            bloom = new BloomComponent(this);
            Components.Add(bloom);

            

            #region CARS_WHEELS_PAINTS

            vehicles[0] = "BMW 5 Series";
            vehicles[1] = "Audi R8 V12";
            vehicles[2] = "1971 Dodge Challenger";
            vehicles[3] = "1966 Ford Mustang GT";
            vehicles[4] = "Lamboghini Gallardo LP 560-4";
            vehicles[5] = "Mercedes Benz SLS AMG";
            vehicles[6] = "Porsche Carrera GT";
            vehicles[7] = "Porsche 911 GT2";
            vehicles[8] = "Ford Mustang Cobra 500KR";
            vehicles[9] = "Mitsubishi Evolution X";

            #endregion


            #region Proiles

            const string f1 = "Profiles.txt";

            StreamReader r1 = new StreamReader(f1);
            string line1;
            line1 = r1.ReadLine();
            users = line1.Split('#');
            profileNames = users[0].Split('@');
            carNames = users[1].Split('@');

            r1.Close();

            #endregion

            #region Audio

            audioStat[0] = true;
            audioStat[1] = true;
            audioStat[2] = true;

            #endregion

            #region Video

            videoStat[0] = true;
            videoStat[1] = true;
            videoStat[2] = false;

            SelectedResolutionTab[0] = false;
            SelectedResolutionTab[1] = false;
            SelectedResolutionTab[2] = false;
            SelectedResolutionTab[3] = false;

            const string f = "sample.txt";

            StreamReader r = new StreamReader(f);
            string line;
            line = r.ReadLine();
            string[] words = line.Split('#');
            width = Convert.ToInt32(words[0]);
            height = Convert.ToInt32(words[1]);
            r.Close();

            #endregion

            //graphics.PreferredBackBufferWidth = width;
            //graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;



            for (int i = 0; i < 8; i++)
            {
                playerCountState[i] = false;
            }

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //camera = new Camera(this, new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.Up);
            camera = new Camera(this, new Vector3(0, 0, 20), new Vector3(0, 0, 0), Vector3.Up);
            Components.Add(camera);
            device = graphics.GraphicsDevice;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            myFont = Content.Load<SpriteFont>(@"Fonts\SpriteFont1");
            IPAddress = Content.Load<SpriteFont>(@"Fonts\IP");
            mouse = Content.Load<Texture2D>(@"Textures\Others\mouse");

            #region Audio

            BackgroundMusic = new Audio("Content\\Audio\\Test1\\TestAudio.xgs", "Content\\Audio\\Test1\\Wave Bank.xwb", "Content\\Audio\\Test1\\Sound Bank.xsb", "Background");
            MenuItemSelect = new Audio("Content\\Audio\\Test1\\TestAudio.xgs", "Content\\Audio\\Test1\\Wave Bank.xwb", "Content\\Audio\\Test1\\Sound Bank.xsb", "ItemSelected");
            LogoMusic = new Audio("Content\\Audio\\Test1\\TestAudio.xgs", "Content\\Audio\\Test1\\Wave Bank.xwb", "Content\\Audio\\Test1\\Sound Bank.xsb", "LogoMusic");
            GameNameMusic = new Audio("Content\\Audio\\Test1\\TestAudio.xgs", "Content\\Audio\\Test1\\Wave Bank.xwb", "Content\\Audio\\Test1\\Sound Bank.xsb", "GameNameMusic");
            Brake = new Audio("Content\\Audio\\Test1\\TestAudio.xgs", "Content\\Audio\\Test1\\Wave Bank.xwb", "Content\\Audio\\Test1\\Sound Bank.xsb", "Brake");
            MapBackground = new Audio("Content\\Audio\\Test1\\TestAudio.xgs", "Content\\Audio\\Test1\\Wave Bank.xwb", "Content\\Audio\\Test1\\Sound Bank.xsb", "Map Background");
            ChallengerIdle = new Audio("Content\\Audio\\Test1\\TestAudio.xgs", "Content\\Audio\\Test1\\Wave Bank.xwb", "Content\\Audio\\Test1\\Sound Bank.xsb", "Challenger Idle");
            ChallengerRace = new Audio("Content\\Audio\\Test1\\TestAudio.xgs", "Content\\Audio\\Test1\\Wave Bank.xwb", "Content\\Audio\\Test1\\Sound Bank.xsb", "Challenger Race");
            Fire = new Audio("Content\\Audio\\Test1\\TestAudio.xgs", "Content\\Audio\\Test1\\Wave Bank.xwb", "Content\\Audio\\Test1\\Sound Bank.xsb", "Firing");

            #endregion


            #region Numbers

            numbers[0] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\0"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[1] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\1"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[2] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\2"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[3] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\3"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[4] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\4"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[5] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\5"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[6] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\6"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[7] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\7"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[8] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\8"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[9] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\9"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[10] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\dot"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            numbers[11] = new GameSprite(Content.Load<Texture2D>(@"Textures\Numbers\colon"), new Rectangle(), Vector2.Zero, 3.0f, 1f);

            #endregion


            #region Welcome

            LogoScreen = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Welcome\LogoScreen"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            NameScreen = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Welcome\NameScreen"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            

            #endregion

            #region MainMenu

            MenuBackground_1024 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\MainMenu\Background_Menu"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            btn_StartServer = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\MainMenu\StartServerButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 40), 3.0f, 0.75f);
            btn_JoinGame = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\MainMenu\JoinGameButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 7), 3.0f, 0.75f);
            btn_Profile = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\MainMenu\ProfileButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 3.9f), 3.0f, 0.75f);
            btn_Customize = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\MainMenu\CustomizeButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 2.7f), 3.0f, 0.75f);
            btn_Settings = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\MainMenu\SettingButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 2.06f), 3.0f, 0.75f);
            btn_Help = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\MainMenu\HelpButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 1.67f), 3.0f, 0.75f);
            btn_Credits = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\MainMenu\CreditsButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 1.4f), 3.0f, 0.75f);
            btn_Quit = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\MainMenu\QuitButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 1.2f), 3.0f, 0.75f);
            btn_ServerStarted = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\MainMenu\ServerOnline"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 40), 3.0f, 0.75f);
            
            #endregion

            #region CreateServer

            CreateServerBackground_1024 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\Background_StartServer"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            btn_LaunchServer = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\LauchServerButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2.8f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);
            img_map1 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\mapImage1"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.45f, Window.ClientBounds.Height / 40f), 3.0f, 1f);
            img_map2 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\mapImage2"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.45f, Window.ClientBounds.Height / 40f), 3.0f, 1f);
            img_map3 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\mapImage3"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.45f, Window.ClientBounds.Height / 40f), 3.0f, 1f);
            btn_map1 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\mapName1"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.38f, Window.ClientBounds.Height / 3.2f), 3.0f, 0.75f);
            btn_Sel_map1 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\mapName1Selected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.38f, Window.ClientBounds.Height / 3.2f), 3.0f, 0.75f);
            btn_map2 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\mapName2"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.38f, Window.ClientBounds.Height / 2.3f), 3.0f, 0.75f);
            btn_Sel_map2 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\mapName2Selected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.38f, Window.ClientBounds.Height / 2.3f), 3.0f, 0.75f);
            btn_map3 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\mapName3"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.38f, Window.ClientBounds.Height / 1.8f), 3.0f, 0.75f);
            btn_Sel_map3 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\mapName3Selected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.38f, Window.ClientBounds.Height / 1.8f), 3.0f, 0.75f);
            noOfPlayers = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\CreateServer\noOfPlayers"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 2.8f), 3.0f, 0.75f);
           
            #endregion

            #region JoinGame

            JoinServerBackground_1024 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\JoinServer\Background_JoinServer"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            enterIP = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\JoinServer\enterIP"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 20, Window.ClientBounds.Height / 3.9f), 3.0f, 0.65f);
            btn_JoinServer = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\JoinServer\joinServerButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2.8f, Window.ClientBounds.Height / 1.8f), 3.0f, 1f);
            WaitForServerBackground_1024 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\JoinServer\WaitingForServer"), new Rectangle(), Vector2.Zero, 3.0f, 1f);

            #endregion

            #region Profile

            ProfileBackground_1024 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Profile\Background_Profile"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            ProfileNameChange_1024 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Profile\ChangeNameScreen"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            Slot[0] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Profile\Slot"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 3.5f), 3.0f, 1f);
            Slot[1] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Profile\Slot"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            Slot[2] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Profile\Slot"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 1.7f), 3.0f, 1f);
            btn_Edit[0] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Profile\EditButtonShort"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 3.5f), 3.0f, 1f);
            btn_Edit[1] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Profile\EditButtonShort"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            btn_Edit[2] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Profile\EditButtonShort"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 1.7f), 3.0f, 1f);
            btn_Save = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Profile\SaveButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3, Window.ClientBounds.Height / 1.2f), 3.0f, 1f);

            #endregion

            #region Customizations

            CustomizeBackground = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\Customize_Background"), new Rectangle(), Vector2.Zero, 3.0f, 1f);

            carChoice[0] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_choice1"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 200f, Window.ClientBounds.Height / 6f), 3.0f, 1f);
            carChoice[1] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_choice2"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 200f, Window.ClientBounds.Height / 3.2f), 3.0f, 1f);
            carChoice[2] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_choice3"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 200f, Window.ClientBounds.Height / 2.18f), 3.0f, 1f);
            carChoice[3] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_choice4"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 200f, Window.ClientBounds.Height / 1.65f), 3.0f, 1f);
            carChoice[4] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_choice5"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 200f, Window.ClientBounds.Height / 1.33f), 3.0f, 1f);
            carChoice[5] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_choice6"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.25f, Window.ClientBounds.Height / 6f), 3.0f, 1f);
            carChoice[6] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_choice7"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.25f, Window.ClientBounds.Height / 3.2f), 3.0f, 1f);
            carChoice[7] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_choice8"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.25f, Window.ClientBounds.Height / 2.18f), 3.0f, 1f);
            carChoice[8] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_choice9"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.25f, Window.ClientBounds.Height / 1.65f), 3.0f, 1f);
            carChoice[9] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_choice10"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.25f, Window.ClientBounds.Height / 1.33f), 3.0f, 1f);

            carChoiceSelected[0] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\car_choice1"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 200f, Window.ClientBounds.Height / 6f), 3.0f, 1f);
            carChoiceSelected[1] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\car_choice2"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 200f, Window.ClientBounds.Height / 3.2f), 3.0f, 1f);
            carChoiceSelected[2] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\car_choice3"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 200f, Window.ClientBounds.Height / 2.18f), 3.0f, 1f);
            carChoiceSelected[3] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\car_choice4"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 200f, Window.ClientBounds.Height / 1.65f), 3.0f, 1f);
            carChoiceSelected[4] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\car_choice5"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 200f, Window.ClientBounds.Height / 1.33f), 3.0f, 1f);
            carChoiceSelected[5] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\car_choice6"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.25f, Window.ClientBounds.Height / 6f), 3.0f, 1f);
            carChoiceSelected[6] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\car_choice7"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.25f, Window.ClientBounds.Height / 3.2f), 3.0f, 1f);
            carChoiceSelected[7] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\car_choice8"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.25f, Window.ClientBounds.Height / 2.18f), 3.0f, 1f);
            carChoiceSelected[8] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\car_choice9"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.25f, Window.ClientBounds.Height / 1.65f), 3.0f, 1f);
            carChoiceSelected[9] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\car_choice10"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.25f, Window.ClientBounds.Height / 1.33f), 3.0f, 1f);

            carInfo[0] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_performance1"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 4.5f, Window.ClientBounds.Height / 3f), 3.0f, 1f);
            carInfo[1] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_performance2"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 4.5f, Window.ClientBounds.Height / 3f), 3.0f, 1f);
            carInfo[2] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_performance3"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 4.5f, Window.ClientBounds.Height / 3f), 3.0f, 1f);
            carInfo[3] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_performance4"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 4.5f, Window.ClientBounds.Height / 3f), 3.0f, 1f);
            carInfo[4] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_performance5"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 4.5f, Window.ClientBounds.Height / 3f), 3.0f, 1f);
            carInfo[5] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_performance6"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 4.5f, Window.ClientBounds.Height / 3f), 3.0f, 1f);
            carInfo[6] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_performance7"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 4.5f, Window.ClientBounds.Height / 3f), 3.0f, 1f);
            carInfo[7] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_performance8"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 4.5f, Window.ClientBounds.Height / 3f), 3.0f, 1f);
            carInfo[8] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_performance9"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 4.5f, Window.ClientBounds.Height / 3f), 3.0f, 1f);
            carInfo[9] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\car_performance10"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 4.5f, Window.ClientBounds.Height / 3f), 3.0f, 1f);

            confirmCarButton = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\confirmButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.8f, Window.ClientBounds.Height / 20f), 3.0f, 1f);
            confirmCarSelectedButton = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Customize\selected\confirmButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.8f, Window.ClientBounds.Height / 20f), 3.0f, 1f);

            
            #endregion

            #region Settings

            Background_Settings = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Background_Settings"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            AudioButton = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\AudioButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width /3f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            VideoButton = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\VideoButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            ControlsButton = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\ControlsButton"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);
            AudioButtonSelected = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\AudioButton_Sel"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            VideoButtonSelected = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\VideoButton_Sel"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            ControlsButtonSelected = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\ControlsButton_Sel"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);
            

            #endregion

            #region Controls

            Background_Controls = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Controls\Background_Controls"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            option1 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Controls\option1"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 30f, Window.ClientBounds.Height / 6f), 3.0f, 1f);
            option2 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Controls\option2"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.9f, Window.ClientBounds.Height / 6f), 3.0f, 1f);
            option1_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Controls\option1_Sel"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 30f, Window.ClientBounds.Height / 6f), 3.0f, 1f);
            option2_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Controls\option2_Sel"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.9f, Window.ClientBounds.Height / 6f), 3.0f, 1f);
            

            #endregion

            #region Audio_Settings

            Background_Audio = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\Background_Audio"), new Rectangle(), Vector2.Zero, 3.0f, 1f);

            BackMusic = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\TrackMusic"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 10f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            EngineNoise = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\EngineNoise"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 10f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            EnvSounds = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\EnvironmentSound"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 10f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);

            BackMusicON = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\on"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            BackMusicON_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\onSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            EngineNoiseON = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\on"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            EngineNoiseON_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\onSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            EnvSoundON = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\on"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);
            EnvSoundON_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\onSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);

            BackMusicOFF = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\off"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            BackMusicOFF_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\offSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            EngineNoiseOFF = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\off"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            EngineNoiseOFF_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\offSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            EnvSoundOFF = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\off"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);
            EnvSoundOFF_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\offSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);

            #endregion

            #region Video_Settings

            Background_Video = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\Background_Video"), new Rectangle(), Vector2.Zero, 3.0f, 1f);
            Background_Video_Res = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\Background_Video_Resolution"), new Rectangle(), Vector2.Zero, 3.0f, 1f);

            PostProcessing = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\PostProcessing"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 10f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            ParticleSystem = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\ParticleSystem"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 10f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            MotionBlur = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\Blur"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 10f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);
            Resolution = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\Resolution"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 1.2f), 3.0f, 1f);

            PostProcessingON = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\on"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            PostProcessingON_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\onSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            ParticleSystemON = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\on"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            ParticleSystemON_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\onSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            MotionBlurON = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\on"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);
            MotionBlurON_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\onSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);

            PostProcessingOFF = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\off"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            PostProcessingOFF_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\offSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            ParticleSystemOFF = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\off"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            ParticleSystemOFF_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\offSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            MotionBlurOFF = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\off"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);
            MotionBlurOFF_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Audio\offSelected"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.5f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);

            Res768 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\768Res"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            Res720 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\720Res"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            Res1080 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\1080Res"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);
            Res1440 = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\1440Res"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 1.2f), 3.0f, 1f);
            Res768_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\768Res_Sel"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 5f), 3.0f, 1f);
            Res720_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\720Res_Sel"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 2.3f), 3.0f, 1f);
            Res1080_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\1080Res_Sel"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 1.5f), 3.0f, 1f);
            Res1440_Sel = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Settings\Video\1440Res_Sel"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 3f, Window.ClientBounds.Height / 1.2f), 3.0f, 1f);


            #endregion

            #region Credits

            CreditsScreen = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Credits"), new Rectangle(), Vector2.Zero, 0f, 1f);

            #endregion

            #region load_Particles

            //load particle stuff

                explotionTexture = Content.Load<Texture2D>(@"Textures\Others\Particle");
            explosionColorsTexture = Content.Load<Texture2D>(@"Textures\Others\ParticleColors");
            explosionEffect = Content.Load<Effect>(@"Effects\Particle");

            //set effect oaraneters that dont change per particle

            explosionEffect.CurrentTechnique = explosionEffect.Techniques["Technique1"];
            explosionEffect.Parameters["theTexture"].SetValue(explotionTexture);

            #endregion

            #region For_All_Maps

            overlayFrame = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\OverlayFrame"), new Rectangle(), Vector2.Zero, 0f, 1f);
            speedMeter = Content.Load<SpriteFont>(@"Fonts\speedometer");

            //speedometers

            //speedoMeters[0] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Meters\Green"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 60f, Window.ClientBounds.Height / 1.48f), 0f, 0.6f);
            //speedoMeters[1] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Meters\Yellow"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 60f, Window.ClientBounds.Height / 1.48f), 0f, 0.6f);
            //speedoMeters[2] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Meters\Red"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 60f, Window.ClientBounds.Height / 1.48f), 0f, 0.6f);
            speedoMeters[0] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Meters\Green"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.35f, Window.ClientBounds.Height / 1.48f), 0f, 0.6f);
            speedoMeters[1] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Meters\Yellow"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.35f, Window.ClientBounds.Height / 1.48f), 0f, 0.6f);
            speedoMeters[2] = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Meters\Red"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 1.35f, Window.ClientBounds.Height / 1.48f), 0f, 0.6f);


            crosshair = new GameSprite(Content.Load<Texture2D>(@"Textures\MenuTextures\1024_768\Crosshair"), new Rectangle(), new Vector2(Window.ClientBounds.Width / 2.1f, Window.ClientBounds.Height / 2f), 0f, 0.2f);


            #endregion

            #region Map1

            //Lasitha write your codes here to load models for Map 1


            mud1 = new SimpleModel(Content.Load<Model>(@"Models\terrain\Mud"), new Vector3(15, 0, -1), 18f, 18f, 1f);
            road1 = new SimpleModel(Content.Load<Model>(@"Models\terrain\Road"), new Vector3(15, 0, 0), 14f, 14f, 1f);

            //cars                                                                                             y   z   x
            bcar1 = new SimpleModel(Content.Load<Model>(@"Models\vehicles\block_car\Block_Cars"), new Vector3(10, 0, -10), 0.5f, 0.5f, 0.5f);
            bcar2 = new SimpleModel(Content.Load<Model>(@"Models\vehicles\block_car\Block_Cars"), new Vector3(30, 0, -40), 0.5f, 0.5f, 0.5f);
            bcar3 = new SimpleModel(Content.Load<Model>(@"Models\vehicles\block_car\Block_Cars"), new Vector3(-30, 0, 30), 0.5f, 0.5f, 0.5f);
            bcar4 = new SimpleModel(Content.Load<Model>(@"Models\vehicles\block_car\Block_Cars"), new Vector3(-70, 0, 10), 0.5f, 0.5f, 0.5f);
            bcar5 = new SimpleModel(Content.Load<Model>(@"Models\vehicles\block_car\Block_Cars"), new Vector3(-67, 0, 6), 0.5f, 0.5f, 0.5f);

            //walls
            //left right
            float pos_sides = 2.2f;
            for (int i = 0; i < wall_right.Length; i++)
            {
                wall_right[i] = new SimpleModel(Content.Load<Model>(@"Models\walls\wall\side2"), new Vector3(-pos_sides, -333, -.5f), 1f, 1f, 1f);
                wall_left[i] = new SimpleModel(Content.Load<Model>(@"Models\walls\wall\side2"), new Vector3(-pos_sides, 360, -.5f), 1f, 1f, 1f);
                pos_sides += 2;
            }

            //up down

            float pos_front = 11.5f;
            for (int i = 0; i < wall_right.Length; i++)
            {
                wall_up[i] = new SimpleModel(Content.Load<Model>(@"Models\walls\wall\side3"), new Vector3(pos_front, -37, -.5f), 1f, 1f, 1f);
                wall_down[i] = new SimpleModel(Content.Load<Model>(@"Models\walls\wall\side3"), new Vector3(pos_front, -730, -.5f), 1f, 1f, 1f);
                pos_front -= 2;
            }





            //rotate by 90 always required for car
            bcar1.rotateZ90();
            bcar2.rotateZ90();
            bcar3.rotateZ90();
            bcar4.rotateZ90();
            bcar5.rotateZ90();

            //rotate 
            bcar3.rotateX90();
            bcar4.rotateX90();
            bcar5.rotateX90();

            //prasad

            //vr = new Vehicle(Content.Load<Model>(@"Models\vehicles\L200\L200-FBX"), new Vector3(0, 0, 0), new Vector3(0.005f, 0.005f, 0.005f));

            vr = new Vehicle(Content.Load<Model>(@"Models\vehicles\Open_Jeep\Open_Jeep"), new Vector3(0, 0, 0), new Vector3(1f, 1f, 1f), "Challenger");
            tr1 = new Tyre(Content.Load<Model>(@"Models\car_tires\Tire_Right"), new Vector3(vehX + 0.38f, vehY + 0.69f, vehZ - 0.76f), 0.1f);
            tr2 = new Tyre(Content.Load<Model>(@"Models\car_tires\Tire_Right"), new Vector3(vehX + 0.38f, vehY + 0.69f, vehZ + 0.62f), 0.1f);

            tl1 = new Tyre(Content.Load<Model>(@"Models\car_tires\Tire_Left"), new Vector3(vehX - 0.38f, vehY + 0.69f, vehZ - 0.76f), 0.1f);
            tl2 = new Tyre(Content.Load<Model>(@"Models\car_tires\Tire_Left"), new Vector3(vehX - 0.38f, vehY + 0.69f, vehZ + 0.62f), 0.1f);

            //vr = new Vehicle(Content.Load<Model>(@"Models\Trees\Avocado\tree"), new Vector3(0, 0, -10), new Vector3(1f, 1f, 1f), "Challenger");
            //vr.rotateZ90();
            //vr.rotateX90();

            //Junk tyres
            tire_in1 = new SimpleModel(Content.Load<Model>(@"Models\tires\Tire_Set2"), new Vector3(0, 0, -180), 1f, 1f, 1f);
            tire_in2 = new SimpleModel(Content.Load<Model>(@"Models\tires\Tire_Set2"), new Vector3(0, -500, -300), 1f, 1f, 1f);
            tire_in3 = new SimpleModel(Content.Load<Model>(@"Models\tires\Tire_Set"), new Vector3(0, 225, -400), 1f, 1f, 1f);

            tire_out1 = new SimpleModel(Content.Load<Model>(@"Models\tires\Tire_Set"), new Vector3(0, -300, -200), 1f, 1f, 1f);
            tire_out2 = new SimpleModel(Content.Load<Model>(@"Models\tires\Tire_Set"), new Vector3(0, -300, -500), 1f, 1f, 1f);
            tire_out3 = new SimpleModel(Content.Load<Model>(@"Models\tires\Tire_Set"), new Vector3(0, 350, -300), 1f, 1f, 1f);
            tire_out4 = new SimpleModel(Content.Load<Model>(@"Models\tires\Tire_Set"), new Vector3(0, 350, -500), 1f, 1f, 1f);


            //junk tyre rotate
            tire_in1.rotateZ90();
            tire_in2.rotateZ90();
            tire_in3.rotateZ90();
            tire_out1.rotateZ90();
            tire_out2.rotateZ90();
            tire_out3.rotateZ90();
            tire_out4.rotateZ90();

            //panes
            //                                                                                 y  z   x
            panes[0] = new SimpleModel(Content.Load<Model>(@"Models\Panes\Pane1"), new Vector3(100, 0, 1), 8f, 8f, 8f);
            panes[1] = new SimpleModel(Content.Load<Model>(@"Models\Panes\Pane1"), new Vector3(-4000, 0, 1), 8f, 8f, 8f);
            panes[2] = new SimpleModel(Content.Load<Model>(@"Models\Panes\Pane2"), new Vector3(-2000, 7, 1), 8f, 8f, 8f);
            panes[3] = new SimpleModel(Content.Load<Model>(@"Models\Panes\Pane2"), new Vector3(2000, 7, 1), 8f, 8f, 8f);

            #endregion

            #region Map2

            //Floor

            FloorMap2[0] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(0, 0, 0), 2f, 2f, 2f);
            FloorMap2[1] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(4, 0, 0), 2f, 2f, 2f);
            FloorMap2[2] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(8, 0, 0), 2f, 2f, 2f);
            FloorMap2[3] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(12, 0, 0), 2f, 2f, 2f);
            FloorMap2[4] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(16, 0, 0), 2f, 2f, 2f);
            FloorMap2[5] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(0, 0, -4), 2f, 2f, 2f);
            FloorMap2[6] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(4, 0, -4), 2f, 2f, 2f);
            FloorMap2[7] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(8, 0, -4), 2f, 2f, 2f);
            FloorMap2[8] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(12, 0, -4), 2f, 2f, 2f);
            FloorMap2[9] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(16, 0, -4), 2f, 2f, 2f);

            FloorMap2[10] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(0, 0, -12), 2f, 2f, 2f);
            FloorMap2[11] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(4, 0, -12), 2f, 2f, 2f);
            FloorMap2[12] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(8, 0, -12), 2f, 2f, 2f);
            FloorMap2[13] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(12, 0, -12), 2f, 2f, 2f);
            FloorMap2[14] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(16, 0, -12), 2f, 2f, 2f);
            FloorMap2[15] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(0, 0, -8f), 2f, 2f, 2f);
            FloorMap2[16] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(4, 0, -8), 2f, 2f, 2f);
            FloorMap2[17] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(8, 0, -8), 2f, 2f, 2f);
            FloorMap2[18] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(12, 0, -8), 2f, 2f, 2f);
            FloorMap2[19] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(16, 0, -8), 2f, 2f, 2f);

            FloorMap2[20] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(0, 0, -16f), 2f, 2f, 2f);
            FloorMap2[21] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(4, 0, -16), 2f, 2f, 2f);
            FloorMap2[22] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(8, 0, -16), 2f, 2f, 2f);
            FloorMap2[23] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(12, 0, -16), 2f, 2f, 2f);
            FloorMap2[24] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Floor\AsphaltFloor_Map2"), new Vector3(16, 0, -16), 2f, 2f, 2f);

            //Wall

            //Front wall

            float change = 12.85f;
            float pos = -16f;
            for (int x = 0; x < 18; x++)
            {
                WallMap2[x] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Wall\WallTubed_Map2"), new Vector3(pos, 0, -152), 1f, 1f, 1f);
                pos += change;
            }

            //back wall

            float change2 = 12.85f;
            float pos2 = -200f;
            for (int x = 18; x < 36; x++)
            {
                WallMap2[x] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Wall\WallTubed_Map2"), new Vector3(pos2, 0, -17), 1f, 1f, 1f);
                WallMap2[x].rotateYplus90();
                WallMap2[x].rotateYplus90();
                pos2 += change2;
            }

            //left wall
            float change3 = 12.85f;
            float pos3 = -10.5f;
            for (int x = 36; x < 50; x++)
            {
                WallMap2[x] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Wall\WallTubed_Map2"), new Vector3(pos3, 0, -22), 1f, 1f, 1f);
                WallMap2[x].rotateYplus90();
                pos3 += change3;
            }
            //right wall
            float change4 = 12.85f;
            float pos4 = -148f;
            for (int x = 50; x < 64; x++)
            {
                WallMap2[x] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Wall\WallTubed_Map2"), new Vector3(pos4, 0, -205), 1f, 1f, 1f);
                WallMap2[x].rotateYminus90();
                pos4 += change4;
            }


            //trees

            TreeMap2[0] = new SimpleModel(Content.Load<Model>(@"Models\Trees\Avocado\tree"), new Vector3(0, 0, -130), 1f, 1f, 1f);
            TreeMap2[1] = new SimpleModel(Content.Load<Model>(@"Models\Trees\Avocado\tree"), new Vector3(100, 0, -130), 1f, 1f, 1f);
            TreeMap2[2] = new SimpleModel(Content.Load<Model>(@"Models\Trees\Avocado\tree"), new Vector3(30, 0, -50), 1f, 1f, 1f);
            TreeMap2[3] = new SimpleModel(Content.Load<Model>(@"Models\Trees\Avocado\tree"), new Vector3(110, 0, -20), 1f, 1f, 1f);
            TreeMap2[4] = new SimpleModel(Content.Load<Model>(@"Models\Trees\Avocado\tree"), new Vector3(200, 0, -100), 1f, 1f, 1f);

            //panes

            PaneMap2[0] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Panes\pane1_Map2"), new Vector3(80, 30, -250), 8f, 8f, 8f);
            PaneMap2[1] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Panes\pane2_Map2"), new Vector3(90, 30, 100), 8f, 8f, 8f);
            PaneMap2[2] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Panes\pane3_Map2"), new Vector3(-148, 30, 0), 8f, 8f, 8f);
            PaneMap2[3] = new SimpleModel(Content.Load<Model>(@"Models\Map2\Panes\pane4_Map2"), new Vector3(310, 30, 0), 8f, 8f, 8f);

            //broken cars

            BrokenCarMap2[0] = new SimpleModel(Content.Load<Model>(@"Models\Vehicles\block_car\BlockCar"), new Vector3(80, -0.7f, -50), 0.5f, 0.5f, 0.5f);
            BrokenCarMap2[1] = new SimpleModel(Content.Load<Model>(@"Models\Vehicles\block_car\BlockCar"), new Vector3(10, -0.7f, 0), 0.5f, 0.5f, 0.5f);
            BrokenCarMap2[2] = new SimpleModel(Content.Load<Model>(@"Models\Vehicles\block_car\BlockCar"), new Vector3(200, -0.7f, -30), 0.5f, 0.5f, 0.5f);


            //junk tires
            JunkTireMap2[0] = new SimpleModel(Content.Load<Model>(@"Models\tires\JunkTire"), new Vector3(0, 0, -80), 1f, 1f, 1f);
            JunkTireMap2[1] = new SimpleModel(Content.Load<Model>(@"Models\tires\JunkTire"), new Vector3(50, 0, 0), 1f, 1f, 1f);
            JunkTireMap2[2] = new SimpleModel(Content.Load<Model>(@"Models\tires\JunkTire"), new Vector3(100, 0, -100), 1f, 1f, 1f);
            JunkTireMap2[3] = new SimpleModel(Content.Load<Model>(@"Models\tires\JunkTire"), new Vector3(150, 0, -80), 1f, 1f, 1f);
            


            #endregion

            currentState = ScreenState.Logo;


            //disable back face culling

            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rs;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //update inputs when server starts
            if(clientStarted)
                MyClient.GetInputAndSendItToServer();

            //change CREATE_SERVER to SERVER_ONLINE and update server
            if (serverStarted)
            {
                server.updateServer();
                btn_StartServer.gamePosition = new Vector2(5000, 5000);
            }
            


            #region SetMyCar


                if (carNames[0] == "0")
                    myCars[0] = vehicles[0];
                if (carNames[0] == "1")
                    myCars[0] = vehicles[1];
                if (carNames[0] == "2")
                    myCars[0] = vehicles[2];
                if (carNames[0] == "3")
                    myCars[0] = vehicles[3];
                if (carNames[0] == "4")
                    myCars[0] = vehicles[4];
                if (carNames[0] == "5")
                    myCars[0] = vehicles[5];
                if (carNames[0] == "6")
                    myCars[0] = vehicles[6];
                if (carNames[0] == "7")
                    myCars[0] = vehicles[7];
                if (carNames[0] == "8")
                    myCars[0] = vehicles[8];
                if (carNames[0] == "9")
                    myCars[0] = vehicles[9];

                if (carNames[1] == "0")
                    myCars[1] = vehicles[0];
                if (carNames[1] == "1")
                    myCars[1] = vehicles[1];
                if (carNames[1] == "2")
                    myCars[1] = vehicles[2];
                if (carNames[1] == "3")
                    myCars[1] = vehicles[3];
                if (carNames[1] == "4")
                    myCars[1] = vehicles[4];
                if (carNames[1] == "5")
                    myCars[1] = vehicles[5];
                if (carNames[1] == "6")
                    myCars[1] = vehicles[6];
                if (carNames[1] == "7")
                    myCars[1] = vehicles[7];
                if (carNames[1] == "8")
                    myCars[1] = vehicles[8];
                if (carNames[1] == "9")
                    myCars[1] = vehicles[9];

                if (carNames[2] == "0")
                    myCars[2] = vehicles[0];
                if (carNames[2] == "1")
                    myCars[2] = vehicles[1];
                if (carNames[2] == "2")
                    myCars[2] = vehicles[2];
                if (carNames[2] == "3")
                    myCars[2] = vehicles[3];
                if (carNames[2] == "4")
                    myCars[2] = vehicles[4];
                if (carNames[2] == "5")
                    myCars[2] = vehicles[5];
                if (carNames[2] == "6")
                    myCars[2] = vehicles[6];
                if (carNames[2] == "7")
                    myCars[2] = vehicles[7];
                if (carNames[2] == "8")
                    myCars[2] = vehicles[8];
                if (carNames[2] == "9")
                    myCars[2] = vehicles[9];


            #endregion

            #region Single_Keyboard_Press_UPDATE

            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            #endregion

            #region Mouse_UPDATE
            mouseRect = new Rectangle((int)mousePos.X, (int)mousePos.Y, (int)mouse.Width, (int)mouse.Height);

            mouseState = Mouse.GetState();
            if (mouseState.X != prevMouseState.X || mouseState.Y != prevMouseState.Y)
                mousePos = new Vector2(mouseState.X, mouseState.Y);
            prevMouseState = mouseState;

            #endregion

    
            
            


                switch (currentState)
                {

                    case ScreenState.Logo:
                        {

                            //play audio
                            LogoMusic.PlayContinues();

                            LogoScreen.Update();

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            if ((currentKeyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter)))
                            {
                                MenuItemSelect.Play();
                                currentState = ScreenState.GameName;
                            }

                            break;
                        }

                    case ScreenState.GameName:
                        {
                            //play audio
                            GameNameMusic.PlayContinues();

                            NameScreen.Update();

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;
                            #endregion

                            if ((currentKeyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter)))
                            {
                                MenuItemSelect.Play();
                                currentState = ScreenState.MainMenu;
                            }

                            break;
                        }
                    case ScreenState.MainMenu:
                        {

                            if (audioStat[0])
                                BackgroundMusic.PlayContinues();
                            else
                                BackgroundMusic.PauseContinues();

                            MapBackground.PauseContinues();
                            ChallengerIdle.PauseContinues();
                            ChallengerRace.PauseContinues();

                            //BackgroundMusic.updateStates();

                            #region Main_Menu_Sprite_Updates
                            MenuBackground_1024.Update();
                            btn_StartServer.Update();
                            btn_JoinGame.Update();
                            btn_Profile.Update();
                            btn_Customize.Update();
                            btn_Settings.Update();
                            btn_Help.Update();
                            btn_Credits.Update();
                            btn_Quit.Update();



                            #endregion

                            Links_MainMenu();

                            //reset profile upon backwarding
                            NameEditMode = false;


                            //if ((currentKeyboardState.IsKeyDown(Keys.O)))
                            //    BackgroundMusic.PlayContinues();
                            //if (currentKeyboardState.IsKeyUp(Keys.O))
                            //    BackgroundMusic.PauseContinues();

                            


                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            break;
                        }

                    case ScreenState.CreateServer:
                        {
                            

                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.MainMenu;

                            #region Create_Server_Sprite_Updates

                            CreateServerBackground_1024.Update();
                            btn_LaunchServer.Update();
                            img_map1.Update();
                            img_map2.Update();
                            img_map3.Update();
                            btn_map1.Update();
                            btn_Sel_map1.Update();
                            btn_map2.Update();
                            btn_Sel_map2.Update();
                            btn_map3.Update();
                            btn_Sel_map3.Update();
                            noOfPlayers.Update();

                            for (int x = 0; x < 12; x++)
                                numbers[x].gamePosition = new Vector2(Window.ClientBounds.Width / 2.8f, Window.ClientBounds.Height / 2.8f);

                            #endregion
                            
                            updateMapSelection();
                            updatePlayerCountSelection();

                            if(mouseRect.Intersects(btn_LaunchServer.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                server = new MyServer();
                                serverStarted = true;
                                currentState = ScreenState.MainMenu;
                            }

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            break;
                        }

                    case ScreenState.JoinGame:
                        {
                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.MainMenu;

                            btn_JoinServer.Update();

                            #region TypingIPAddress

                            if ((currentKeyboardState.IsKeyDown(Keys.NumPad0) &&
                                 lastKeyboardState.IsKeyUp(Keys.NumPad0)))
                            {
                                joinIP += "0";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.NumPad1) &&
                                 lastKeyboardState.IsKeyUp(Keys.NumPad1)))
                            {
                                joinIP += "1";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.NumPad2) &&
                                 lastKeyboardState.IsKeyUp(Keys.NumPad2)))
                            {
                                joinIP += "2";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.NumPad3) &&
                                 lastKeyboardState.IsKeyUp(Keys.NumPad3)))
                            {
                                joinIP += "3";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.NumPad4) &&
                                 lastKeyboardState.IsKeyUp(Keys.NumPad4)))
                            {
                                joinIP += "4";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.NumPad5) &&
                                 lastKeyboardState.IsKeyUp(Keys.NumPad5)))
                            {
                                joinIP += "5";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.NumPad6) &&
                                 lastKeyboardState.IsKeyUp(Keys.NumPad6)))
                            {
                                joinIP += "6";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.NumPad7) &&
                                 lastKeyboardState.IsKeyUp(Keys.NumPad7)))
                            {
                                joinIP += "7";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.NumPad8) &&
                                 lastKeyboardState.IsKeyUp(Keys.NumPad8)))
                            {
                                joinIP += "8";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.NumPad9) &&
                                 lastKeyboardState.IsKeyUp(Keys.NumPad9)))
                            {
                                joinIP += "9";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.OemPeriod) &&
                                 lastKeyboardState.IsKeyUp(Keys.OemPeriod)))
                            {
                                joinIP += ".";
                            }

                            if ((currentKeyboardState.IsKeyDown(Keys.Delete) &&
                                 lastKeyboardState.IsKeyUp(Keys.Delete)))
                            {
                                joinIP = "";
                            }


                            #endregion

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            if (mouseRect.Intersects(btn_JoinServer.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                //run CLient startup code and connect To Code
                                MenuItemSelect.Play();
                                //client = new MyClient();
                                clientStarted = true;
                                currentState = ScreenState.WaitToJoin;
                                
                            }

                            break;
                        }

                    case ScreenState.WaitToJoin:
                        {
                            //use a 5 second timer to show "3,2,1 and GO! message"


                            //then send player to Map directly
                            if(map1_bool)
                                currentState = ScreenState.Map1;
                            if (map2_bool)
                                currentState = ScreenState.Map2;
                            if (map3_bool)
                                currentState = ScreenState.Map2;

                                #region PostProcessing

                                bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            break;
                        }

                    case ScreenState.Lobby:
                        {
                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.MainMenu;

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            break;
                        }

                    case ScreenState.Profile:
                        {
                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.MainMenu;

                            #region Profile_Updates

                            btn_Edit[0].Update();
                            btn_Edit[1].Update();
                            btn_Edit[2].Update();
                            Slot[0].Update();
                            Slot[1].Update();
                            Slot[2].Update();
                            btn_Save.Update();

                            #endregion

                            #region EditCurrentProfile

                            if (mouseRect.Intersects(btn_Edit[0].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                selectedProfile = 1;
                                NameEditMode = true;
                            }

                            if (mouseRect.Intersects(btn_Edit[1].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                selectedProfile = 2;
                                NameEditMode = true;
                            }

                            if (mouseRect.Intersects(btn_Edit[2].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                selectedProfile = 3;
                                NameEditMode = true;
                            }

                            #endregion

                            #region SelectCurrentProfile

                            if (mouseRect.Intersects(Slot[0].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                selectedProfile = 1;
                                currentState = ScreenState.MainMenu;
                            }

                            if (mouseRect.Intersects(Slot[1].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                selectedProfile = 2;
                                currentState = ScreenState.MainMenu;
                            }

                            if (mouseRect.Intersects(Slot[2].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                selectedProfile = 3;
                                currentState = ScreenState.MainMenu;
                            }

                            #endregion

                            #region TypingName

                            if (NameEditMode)
                            {

                                if ((currentKeyboardState.IsKeyDown(Keys.A) && lastKeyboardState.IsKeyUp(Keys.A)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "A";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.B) && lastKeyboardState.IsKeyUp(Keys.B)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "B";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.C) && lastKeyboardState.IsKeyUp(Keys.C)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "C";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.D) && lastKeyboardState.IsKeyUp(Keys.D)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "D";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.E) && lastKeyboardState.IsKeyUp(Keys.E)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "E";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.F) && lastKeyboardState.IsKeyUp(Keys.F)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "F";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.G) && lastKeyboardState.IsKeyUp(Keys.G)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "G";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.H) && lastKeyboardState.IsKeyUp(Keys.H)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "H";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.I) && lastKeyboardState.IsKeyUp(Keys.I)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "I";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.J) && lastKeyboardState.IsKeyUp(Keys.J)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "J";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.K) && lastKeyboardState.IsKeyUp(Keys.K)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "K";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.L) && lastKeyboardState.IsKeyUp(Keys.L)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "L";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.M) && lastKeyboardState.IsKeyUp(Keys.M)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "M";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.N) && lastKeyboardState.IsKeyUp(Keys.N)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "N";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.O) && lastKeyboardState.IsKeyUp(Keys.O)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "O";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.P) && lastKeyboardState.IsKeyUp(Keys.P)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "P";

                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.Q) && lastKeyboardState.IsKeyUp(Keys.Q)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "Q";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.R) && lastKeyboardState.IsKeyUp(Keys.R)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "R";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.S) && lastKeyboardState.IsKeyUp(Keys.S)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "S";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.T) && lastKeyboardState.IsKeyUp(Keys.T)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "T";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.U) && lastKeyboardState.IsKeyUp(Keys.U)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "U";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.V) && lastKeyboardState.IsKeyUp(Keys.V)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "V";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.W) && lastKeyboardState.IsKeyUp(Keys.W)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "W";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.X) && lastKeyboardState.IsKeyUp(Keys.X)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "X";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.Y) && lastKeyboardState.IsKeyUp(Keys.Y)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "Y";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.Z) && lastKeyboardState.IsKeyUp(Keys.Z)))
                                {
                                    MenuItemSelect.Play();
                                    typedName += "Z";
                                }

                                if ((currentKeyboardState.IsKeyDown(Keys.Delete) && lastKeyboardState.IsKeyUp(Keys.Delete)))
                                {
                                    MenuItemSelect.Play();
                                    typedName = "";
                                }
                            }

                            #endregion


                            #region SavingProfileEdits

                            if (mouseRect.Intersects(btn_Save.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                //PRASAD@LASITHA@SEHAN#0@1@2#0@1@2#0@1@2

                                if (selectedProfile == 1)
                                {
                                    string res = typedName + "@" + profileNames[1] + "@" + profileNames[2] + "#" + carNames[0] + "@" + carNames[1] + "@" + carNames[2] + "#";

                                    try
                                    {
                                        using (TextWriter tw = new StreamWriter("Profiles.txt", false))
                                        {
                                            tw.WriteLine(res);
                                            tw.Close();
                                        }
                                    }
                                    catch (Exception ex) { }
                                }

                                if (selectedProfile == 2)
                                {
                                    string res = profileNames[0] + "@" + typedName + "@" + profileNames[2] + "#" + carNames[0] + "@" + carNames[1] + "@" + carNames[2] + "#";

                                    try
                                    {
                                        using (TextWriter tw = new StreamWriter("Profiles.txt", false))
                                        {
                                            tw.WriteLine(res);
                                            tw.Close();
                                        }
                                    }
                                    catch (Exception ex) { }
                                }

                                if (selectedProfile == 3)
                                {
                                    string res = profileNames[0] + "@" + profileNames[1] + "@" + typedName + "#" + carNames[0] + "@" + carNames[1] + "@" + carNames[2] + "#";

                                    try
                                    {
                                        using (TextWriter tw = new StreamWriter("Profiles.txt", false))
                                        {
                                            tw.WriteLine(res);
                                            tw.Close();
                                        }
                                    }
                                    catch (Exception ex) { }
                                }


                                currentState = ScreenState.MainMenu;

                            }

                            #endregion

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            break;
                        }

                    case ScreenState.Customize:
                        {

                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                            {
                                MenuItemSelect.Play();
                                currentState = ScreenState.MainMenu;
                            }

                            UpdateExplosions(gameTime);

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            for (int x = 0; x < 10; x++)
                                carChoice[x].Update();

                            confirmCarButton.Update();
                            confirmCarSelectedButton.Update();

                            if (mouseRect.Intersects(confirmCarButton.getRectangle()))
                                confimMouseOver = true;
                            else
                                confimMouseOver = false;


                            #region InspectCar

                            if (mouseRect.Intersects(carChoice[0].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                confirmedCar = "0";
                            }

                            if (mouseRect.Intersects(carChoice[1].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                confirmedCar = "1";
                            }

                            if (mouseRect.Intersects(carChoice[2].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                confirmedCar = "2";
                            }

                            if (mouseRect.Intersects(carChoice[3].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                confirmedCar = "3";
                            }

                            if (mouseRect.Intersects(carChoice[4].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                confirmedCar = "4";
                            }

                            if (mouseRect.Intersects(carChoice[5].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                confirmedCar = "5";
                            }

                            if (mouseRect.Intersects(carChoice[6].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                confirmedCar = "6";
                            }

                            if (mouseRect.Intersects(carChoice[7].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                confirmedCar = "7";
                            }

                            if (mouseRect.Intersects(carChoice[8].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                confirmedCar = "8";
                            }

                            if (mouseRect.Intersects(carChoice[9].getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                confirmedCar = "9";
                            }


                            #endregion


                            #region Confirm_and_Write_TO_File

                            if ((mouseRect.Intersects(confirmCarButton.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed) || (mouseRect.Intersects(confirmCarSelectedButton.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed))
                            {
                                MenuItemSelect.Play();

                                int x = Int32.Parse(confirmedCar);

                                if (selectedProfile == 1)
                                    carNames[0] = confirmedCar;
                                if (selectedProfile == 2)
                                    carNames[1] = confirmedCar;
                                if (selectedProfile == 3)
                                    carNames[2] = confirmedCar;


                                //write to file


                                string res = profileNames[0] + "@" + profileNames[1] + "@" + profileNames[2] + "#" + carNames[0] + "@" + carNames[1] + "@" + carNames[2] + "#";

                                try
                                {
                                    using (TextWriter tw = new StreamWriter("Profiles.txt", false))
                                    {
                                        tw.WriteLine(res);
                                        tw.Close();
                                    }
                                }
                                catch (Exception ex) { }


                                currentState = ScreenState.MainMenu;


                            }

                            #endregion

                            break;
                        }

                    case ScreenState.SettingsMains:
                        {

                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.MainMenu;

                            UpdateExplosions(gameTime);

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            #region Settings_Main

                            AudioButton.Update();
                            VideoButton.Update();
                            ControlsButton.Update();
                            AudioButtonSelected.Update();
                            VideoButtonSelected.Update();
                            ControlsButtonSelected.Update();

                            #endregion

                            #region Navigation

                            if (mouseRect.Intersects(AudioButton.getRectangle()))
                                SelectedSetting[0] = true;
                            else
                                SelectedSetting[0] = false;

                            if (mouseRect.Intersects(VideoButton.getRectangle()))
                                SelectedSetting[1] = true;
                            else
                                SelectedSetting[1] = false;

                            if (mouseRect.Intersects(ControlsButton.getRectangle()))
                                SelectedSetting[2] = true;
                            else
                                SelectedSetting[2] = false;

                            if ((mouseRect.Intersects(AudioButton.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed) || (mouseRect.Intersects(AudioButtonSelected.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed))
                            {
                                MenuItemSelect.Play();
                                currentState = ScreenState.SettingsAudio;
                            }

                            if ((mouseRect.Intersects(VideoButton.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed) || (mouseRect.Intersects(VideoButtonSelected.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed))
                            {
                                MenuItemSelect.Play();
                                currentState = ScreenState.SettingsVideo;
                            }

                            if ((mouseRect.Intersects(ControlsButton.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed) || (mouseRect.Intersects(ControlsButtonSelected.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed))
                            {
                                MenuItemSelect.Play();
                                currentState = ScreenState.SettingsControls;
                            }

                            #endregion

                            break;
                        }

                    case ScreenState.SettingsAudio:
                        {
                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.SettingsMains;

                            UpdateExplosions(gameTime);

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            #region Audio_Updates

                            BackMusicON.Update();
                            BackMusicON_Sel.Update();
                            EngineNoiseON.Update();
                            EngineNoiseON_Sel.Update();
                            EnvSoundON.Update();
                            EnvSoundON_Sel.Update();

                            BackMusicOFF.Update();
                            BackMusicOFF_Sel.Update();
                            EngineNoiseOFF.Update();
                            EngineNoiseOFF_Sel.Update();
                            EnvSoundOFF.Update();
                            EnvSoundOFF_Sel.Update();

                            #endregion

                            #region Audio_Swiches

                            if (mouseRect.Intersects(BackMusicOFF.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                audioStat[0] = false;
                            }

                            if (mouseRect.Intersects(EngineNoiseOFF.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                audioStat[1] = false;
                            }

                            if (mouseRect.Intersects(EnvSoundOFF.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                audioStat[2] = false;
                            }

                            if (mouseRect.Intersects(BackMusicON.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                audioStat[0] = true;
                            }

                            if (mouseRect.Intersects(EngineNoiseON.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                audioStat[1] = true;
                            }

                            if (mouseRect.Intersects(EnvSoundON.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                audioStat[2] = true;
                            }

                            #endregion

                            break;
                        }

                    case ScreenState.SettingsVideo:
                        {
                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.SettingsMains;

                            UpdateExplosions(gameTime);

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            #region Texture_Updates

                            Resolution.Update();

                            PostProcessingON.Update();
                            PostProcessingON_Sel.Update();
                            ParticleSystemON.Update();
                            ParticleSystemON_Sel.Update();
                            MotionBlurON.Update();
                            MotionBlurON_Sel.Update();

                            PostProcessingOFF.Update();
                            PostProcessingOFF_Sel.Update();
                            ParticleSystemOFF.Update();
                            ParticleSystemOFF_Sel.Update();
                            MotionBlurOFF.Update();
                            MotionBlurOFF_Sel.Update();


                            Res768.Update();
                            Res720.Update();
                            Res1080.Update();
                            Res1440.Update();
                            Res768_Sel.Update();
                            Res720_Sel.Update();
                            Res1080_Sel.Update();
                            Res1440_Sel.Update();


                            #endregion

                            #region Navigation

                            if (mouseRect.Intersects(Res768.getRectangle()))
                                SelectedResolutionTab[0] = true;
                            else
                                SelectedResolutionTab[0] = false;

                            if (mouseRect.Intersects(Res720.getRectangle()))
                                SelectedResolutionTab[1] = true;
                            else
                                SelectedResolutionTab[1] = false;

                            if (mouseRect.Intersects(Res1080.getRectangle()))
                                SelectedResolutionTab[2] = true;
                            else
                                SelectedResolutionTab[2] = false;

                            if (mouseRect.Intersects(Res1440.getRectangle()))
                                SelectedResolutionTab[3] = true;
                            else
                                SelectedResolutionTab[3] = false;



                            if ((mouseRect.Intersects(Res768.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed) || (mouseRect.Intersects(Res768_Sel.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed))
                            {
                                width = 1024;
                                height = 768;
                                ResEditMode = false;

                                string res = width.ToString() + "#" + height.ToString();

                                try
                                {
                                    using (TextWriter tw = new StreamWriter("sample.txt", false))
                                    {
                                        tw.WriteLine(res);
                                        tw.Close();
                                    }
                                }
                                catch (Exception ex) { }

                            }

                            if ((mouseRect.Intersects(Res720.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed) || (mouseRect.Intersects(Res720_Sel.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed))
                            {
                                width = 1280;
                                height = 720;
                                ResEditMode = false;

                                string res = width.ToString() + "#" + height.ToString();

                                try
                                {
                                    using (TextWriter tw = new StreamWriter("sample.txt", false))
                                    {
                                        tw.WriteLine(res);
                                        tw.Close();
                                    }
                                }
                                catch (Exception ex) { }

                            }

                            if ((mouseRect.Intersects(Res1080.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed) || (mouseRect.Intersects(Res1080_Sel.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed))
                            {
                                width = 1920;
                                height = 1080;
                                ResEditMode = false;

                                string res = width.ToString() + "#" + height.ToString();

                                try
                                {
                                    using (TextWriter tw = new StreamWriter("sample.txt", false))
                                    {
                                        tw.WriteLine(res);
                                        tw.Close();
                                    }
                                }
                                catch (Exception ex) { }

                            }

                            if ((mouseRect.Intersects(Res1440.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed) || (mouseRect.Intersects(Res1440_Sel.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed))
                            {
                                width = 2560;
                                height = 1440;
                                ResEditMode = false;

                                string res = width.ToString() + "#" + height.ToString();

                                try
                                {
                                    using (TextWriter tw = new StreamWriter("sample.txt", false))
                                    {
                                        tw.WriteLine(res);
                                        tw.Close();
                                    }
                                }
                                catch (Exception ex) { }

                            }



                            #endregion


                            #region Video_Swiches

                            if (mouseRect.Intersects(PostProcessingOFF.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                videoStat[0] = false;
                            }

                            if (mouseRect.Intersects(ParticleSystemOFF.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                videoStat[1] = false;
                            }

                            if (mouseRect.Intersects(MotionBlurOFF.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                videoStat[2] = false;
                            }

                            if (mouseRect.Intersects(PostProcessingON.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                videoStat[0] = true;
                            }

                            if (mouseRect.Intersects(ParticleSystemON.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                videoStat[1] = true;
                            }

                            if (mouseRect.Intersects(MotionBlurON.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                videoStat[2] = true;
                            }

                            #endregion

                            if (mouseRect.Intersects(Resolution.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
                            {
                                MenuItemSelect.Play();
                                ResEditMode = true;
                            }


                            break;
                        }

                    case ScreenState.SettingsControls:
                        {
                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.SettingsMains;

                            UpdateExplosions(gameTime);

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion

                            #region Control_Settings_Updates

                            option1.Update();
                            option1_Sel.Update();
                            option2.Update();
                            option2_Sel.Update();

                            #endregion

                            #region Navigation

                            if (mouseRect.Intersects(option1.getRectangle()))
                                selectedControl[0] = true;
                            else
                                selectedControl[0] = false;

                            if (mouseRect.Intersects(option2.getRectangle()))
                                selectedControl[1] = true;
                            else
                                selectedControl[1] = false;


                            if ((mouseRect.Intersects(option1.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed) || (mouseRect.Intersects(option1_Sel.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed))
                            {
                                MenuItemSelect.Play();
                                Control = 1;
                                currentState = ScreenState.SettingsMains;
                            }

                            if ((mouseRect.Intersects(option2.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed) || (mouseRect.Intersects(option2_Sel.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed))
                            {
                                MenuItemSelect.Play();
                                Control = 2;
                                currentState = ScreenState.SettingsMains;
                            }

                            #endregion


                            break;
                        }

                    case ScreenState.Credits:
                        {
                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.MainMenu;

                            CreditsScreen.Update();

                            UpdateExplosions(gameTime);

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[0];
                            bloom.Visible = true;

                            #endregion


                            break;
                        }
                    case ScreenState.Map1:
                        {
                            //play map audio
                            BackgroundMusic.PauseContinues();
                            MapBackground.PlayContinues();


                            if (audioStat[2])
                                MapBackground.PlayContinues();
                            else
                                MapBackground.PauseContinues();


                            if ((currentKeyboardState.IsKeyDown(Keys.W)))
                            {
                                ChallengerIdle.PauseContinues();

                                if (audioStat[1])
                                    ChallengerRace.PlayContinues();
                                else
                                    ChallengerRace.PauseContinues();
                            }

                            if (currentKeyboardState.IsKeyUp(Keys.W))
                            {
                                ChallengerRace.PauseContinues();

                                if (audioStat[1])
                                    ChallengerIdle.PlayContinues();
                                else
                                    ChallengerIdle.PauseContinues();
                            }

                            if (currentKeyboardState.IsKeyDown(Keys.S))
                            {
                                ChallengerRace.PauseContinues();

                                if (audioStat[1])
                                    Brake.PlayContinues();
                                else
                                    Brake.PauseContinues();
                            }
                            if (currentKeyboardState.IsKeyUp(Keys.S))
                            {

                                Brake.PauseContinues();
                            }

                            

                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.MainMenu;

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[bloomSettingsIndex];
                            //to add processing.. change false to true 
                            bloom.Visible = false;

                            if (videoStat[0])
                                bloom.Visible = bloom.Visible;
                            else
                                bloom.Visible = !bloom.Visible;

                            if (videoStat[2])
                                BloomSettings.PresetSettings[0].BlurAmount = 10f;
                            else
                                BloomSettings.PresetSettings[0].BlurAmount = 3.5f;

                            #endregion

                            //overlay

                            overlayFrame.Update();

                            for (int x = 0; x < speedoMeters.Length; x++)
                            {
                                speedoMeters[x].Update();
                            }

                            crosshair.Update();

                                //Lasitha write your codes here to update Map1

                                road1.update();
                            mud1.update();

                            bcar1.update();
                            bcar2.update();
                            bcar3.update();
                            bcar4.update();
                            bcar5.update();

                            for (int i = 0; i < wall_right.Length; i++)
                            {
                                wall_right[i].update();
                                wall_left[i].update();
                                wall_up[i].update();
                                wall_down[i].update();
                            }

                            //prasad

                            vr.update(tr1.getSpeed());
                            tr1.setTires(vr.getWorldLoc(), new Vector3(.78f, .39f, -1.54f), gameTime, vr.getQuaRot());
                            tr2.setTires(vr.getWorldLoc(), new Vector3(-.78f, .39f, -1.54f), gameTime, vr.getQuaRot());
                            tl1.setTires(vr.getWorldLoc(), new Vector3(.78f, .39f, 1.22f), gameTime, vr.getQuaRot());
                            tl2.setTires(vr.getWorldLoc(), new Vector3(-.78f, .39f, 1.22f), gameTime, vr.getQuaRot());

                            //junk tyres
                            tire_in1.update();
                            tire_in2.update();
                            tire_in3.update();

                            tire_out1.update();
                            tire_out2.update();
                            tire_out3.update();
                            tire_out4.update();

                            //panes

                            panes[0].update();
                            panes[1].update();
                            panes[2].update();
                            panes[3].update();

                            //update speed

                            if (currentKeyboardState.IsKeyDown(Keys.W))
                            {
                                if (meterSpeed < 101)
                                    meterSpeed += 0.1f;

                                if (meterRpm < 9000)
                                    meterRpm += 5f;
                            }

                            if (currentKeyboardState.IsKeyUp(Keys.W))
                            {
                                if (meterSpeed > 0)
                                    meterSpeed -= 0.8f;

                                if (meterRpm > 1500)
                                    meterRpm -= 200f;
                            }




                            break;
                        }

                    case ScreenState.Map2:
                        {
                            //play map audio
                            
                            BackgroundMusic.PauseContinues();

                            if (audioStat[2])
                                MapBackground.PlayContinues();
                            else
                                MapBackground.PauseContinues();


                            if ((currentKeyboardState.IsKeyDown(Keys.W)))
                            {
                                ChallengerIdle.PauseContinues();

                                if(audioStat[1])
                                    ChallengerRace.PlayContinues();
                                else
                                    ChallengerRace.PauseContinues();
                            }

                            if (currentKeyboardState.IsKeyUp(Keys.W))
                            {
                                ChallengerRace.PauseContinues();

                                if (audioStat[1])
                                    ChallengerIdle.PlayContinues();
                                else
                                    ChallengerIdle.PauseContinues();
                            }

                            if (currentKeyboardState.IsKeyDown(Keys.S))
                            {
                                ChallengerRace.PauseContinues();

                                if (audioStat[1])
                                    Brake.PlayContinues();
                                else
                                    Brake.PauseContinues();
                            }

                            if (currentKeyboardState.IsKeyUp(Keys.S))
                            {
                                Brake.PauseContinues();
                            }

                            if (currentKeyboardState.IsKeyDown(Keys.Space))
                            {
                                Fire.PlayContinues();
                
                            }
                            if (currentKeyboardState.IsKeyUp(Keys.Space))
                            {
                                Fire.PauseContinues();
                            }



                            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                                currentState = ScreenState.MainMenu;

                            //overlay

                            overlayFrame.Update();
                            crosshair.Update();

                            for (int x = 0; x < speedoMeters.Length; x++)
                            {
                                speedoMeters[x].Update();
                            }

                            #region PostProcessing

                            bloom.Settings = BloomSettings.PresetSettings[bloomSettingsIndex];
                            //to add processing.. change false to true 
                            bloom.Visible = true;

                            if (videoStat[0])
                                bloom.Visible = bloom.Visible;
                            else
                                bloom.Visible = !bloom.Visible;

                            if (videoStat[2])
                                BloomSettings.PresetSettings[0].BlurAmount = 10f;
                            else
                                BloomSettings.PresetSettings[0].BlurAmount = 3.5f;

                            #endregion

                           
                            //Update floors

                            for (int i = 0; i < FloorMap2.Length; i++)
                            {
                                FloorMap2[i].update();
                            }

                            //update walls

                            for (int i = 0; i < WallMap2.Length; i++)
                            {
                                WallMap2[i].update();
                            }

                            //update trees

                            for (int i = 0; i < TreeMap2.Length; i++)
                            {
                                TreeMap2[i].update();
                            }

                            //panes

                            for (int i = 0; i < PaneMap2.Length; i++)
                            {
                                PaneMap2[i].update();
                            }

                            //junk tires

                            for (int i = 0; i < JunkTireMap2.Length; i++)
                            {
                                JunkTireMap2[i].update();
                            }

                            //broken cars

                            for (int i = 0; i < BrokenCarMap2.Length; i++)
                            {
                                BrokenCarMap2[i].update();
                            }


                            //prasad

                            vr.update(tr1.getSpeed());
                            tr1.setTires(vr.getWorldLoc(), new Vector3(.78f, .39f, -1.54f), gameTime, vr.getQuaRot());
                            tr2.setTires(vr.getWorldLoc(), new Vector3(-.78f, .39f, -1.54f), gameTime, vr.getQuaRot());
                            tl1.setTires(vr.getWorldLoc(), new Vector3(.78f, .39f, 1.22f), gameTime, vr.getQuaRot());
                            tl2.setTires(vr.getWorldLoc(), new Vector3(-.78f, .39f, 1.22f), gameTime, vr.getQuaRot());

                            //update speed

                            if (currentKeyboardState.IsKeyDown(Keys.W))
                            {
                                if(meterSpeed < 101)
                                    meterSpeed += 0.1f;

                                if (meterRpm < 9000)
                                    meterRpm += 5f;
                            }

                            if (currentKeyboardState.IsKeyUp(Keys.W))
                            {
                                if (meterSpeed > 0)
                                    meterSpeed -= 0.8f;

                                if (meterRpm > 1500)
                                    meterRpm -= 200f;
                            }

   

                            break;
                        }


                }

            
            

            

            //if (collide())
            //{
            //    // add code for collision 

            //    explosions.Add(new ParticleExplosion(GraphicsDevice, s2.getMyWorld().Translation, rnd.Next(partcleExpSettings.minLife, partcleExpSettings.maxLife), rnd.Next(partcleExpSettings.minRoundTime, partcleExpSettings.maxRoundTime), rnd.Next(partcleExpSettings.minPerRound, partcleExpSettings.maxPerRound), rnd.Next(partcleExpSettings.minParticles, partcleExpSettings.maxParticles), explosionColorsTexture, particleSettings, explosionEffect));
            //}

            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            // TODO: Add your drawing code here


            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.Draw(mouse, mousePos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1f);

            switch (currentState)
            {
                case ScreenState.Logo:
                    {
                        bloom.BeginDraw();
                        LogoScreen.DrawBack(spriteBatch);

                      //  spriteBatch.DrawString(myFont, profileNames[0] + " " + profileNames[1] + " " + profileNames[2] + " " + myCars[0] + " " + myCars[1] + " " + myCars[2], new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 2)), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                       
                        break;
                    }

                case ScreenState.GameName:
                    {
                        bloom.BeginDraw();
                        NameScreen.DrawBack(spriteBatch);
                        break;
                    }
                case ScreenState.MainMenu:
                    {
                        bloom.BeginDraw();
 
                        #region MainMenu_Draw

                        MenuBackground_1024.DrawBack(spriteBatch);

                        if (!serverStarted)
                            btn_StartServer.Draw(spriteBatch);
                        else
                            btn_ServerStarted.Draw(spriteBatch);

                        btn_JoinGame.Draw(spriteBatch);
                        btn_Profile.Draw(spriteBatch);
                        btn_Customize.Draw(spriteBatch);
                        btn_Settings.Draw(spriteBatch);
                        btn_Help.Draw(spriteBatch);
                        btn_Credits.Draw(spriteBatch);
                        btn_Quit.Draw(spriteBatch);

                        #endregion

                        spriteBatch.DrawString(IPAddress, "Welcome " + profileNames[selectedProfile-1]+ "! Let's Fight!" , new Vector2((Window.ClientBounds.Width / 3.2f), (Window.ClientBounds.Height / 1.08f)), Color.LightBlue, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 1);
                       
                        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                        break;
                    }

                case ScreenState.CreateServer:
                    {
                        bloom.BeginDraw();

                        CreateServerBackground_1024.DrawBack(spriteBatch);
                        btn_LaunchServer.Draw(spriteBatch);

                        #region Print_MAPS_MAPICONS

                        if (map1_bool)
                            img_map1.Draw(spriteBatch);
                        if(map2_bool)
                            img_map2.Draw(spriteBatch);
                        if (map3_bool)
                            img_map3.Draw(spriteBatch);

                        btn_map1.Draw(spriteBatch);

                        if (map1_bool)
                            btn_Sel_map1.DrawSelected(spriteBatch);
                        btn_map2.Draw(spriteBatch);
                        if (map2_bool)
                            btn_Sel_map2.DrawSelected(spriteBatch);
                        btn_map3.Draw(spriteBatch);
                        if (map3_bool)
                            btn_Sel_map3.DrawSelected(spriteBatch);

                        noOfPlayers.Draw(spriteBatch);

                        #endregion

                        #region Print_PlayerCount

                        if (playerCount == 0)
                            numbers[0].Draw(spriteBatch);
                        if (playerCount == 1)
                            numbers[1].Draw(spriteBatch);
                        if (playerCount == 2)
                            numbers[2].Draw(spriteBatch);
                        if (playerCount == 3)
                            numbers[3].Draw(spriteBatch);
                        if (playerCount == 4)
                            numbers[4].Draw(spriteBatch);
                        if (playerCount == 5)
                            numbers[5].Draw(spriteBatch);
                        if (playerCount == 6)
                            numbers[6].Draw(spriteBatch);
                        if (playerCount == 7)
                            numbers[7].Draw(spriteBatch);
                        if (playerCount == 8)
                            numbers[8].Draw(spriteBatch);

                        #endregion

                        break;
                    }

                case ScreenState.JoinGame:
                    {
                        bloom.BeginDraw();
                        JoinServerBackground_1024.DrawBack(spriteBatch);
                        enterIP.Draw(spriteBatch);
                        btn_JoinServer.Draw(spriteBatch);

                        spriteBatch.DrawString(IPAddress, joinIP, new Vector2((Window.ClientBounds.Width / 4), (Window.ClientBounds.Height / 4)), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        
                        break;
                    }

                case ScreenState.WaitToJoin:
                    {
                        bloom.BeginDraw();
                        WaitForServerBackground_1024.DrawBack(spriteBatch);
                        break;
                    }

                case ScreenState.Lobby:
                    {
                        bloom.BeginDraw();
                        break;
                    }

                case ScreenState.Profile:
                    {
                        bloom.BeginDraw();

                        #region Profile_Draw

                        ProfileBackground_1024.DrawBack(spriteBatch);

                        if (NameEditMode)
                        {
                            ProfileNameChange_1024.Draw(spriteBatch);
                            btn_Save.Draw(spriteBatch);
                            spriteBatch.DrawString(IPAddress,typedName, new Vector2((Window.ClientBounds.Width / 3), (Window.ClientBounds.Height / 3)), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }

                        if (!NameEditMode)
                        {
                            Slot[0].Draw(spriteBatch);
                            Slot[1].Draw(spriteBatch);
                            Slot[2].Draw(spriteBatch);
                            btn_Edit[0].Draw(spriteBatch);
                            btn_Edit[1].Draw(spriteBatch);
                            btn_Edit[2].Draw(spriteBatch);

                            //font

                            spriteBatch.DrawString(IPAddress, profileNames[0],new Vector2((Window.ClientBounds.Width / 3), (Window.ClientBounds.Height / 3.5f)), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                            spriteBatch.DrawString(IPAddress, profileNames[1], new Vector2((Window.ClientBounds.Width / 3), (Window.ClientBounds.Height / 2.3f)), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                            spriteBatch.DrawString(IPAddress, profileNames[2], new Vector2((Window.ClientBounds.Width / 3), (Window.ClientBounds.Height / 1.7f)), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                        }

                        #endregion

                        break;
                    }

                case ScreenState.Customize:
                    {
                        bloom.BeginDraw();

                        CustomizeBackground.DrawBack(spriteBatch);

                        for (int x = 0; x < 10; x++)
                        {
                            carChoice[x].Draw(spriteBatch);
                            //carInfo[x].Draw(spriteBatch);
                        }

                        #region show+CarInfo_Hightlight_selected

                        if (confirmedCar == "0")
                        {
                            carInfo[0].Draw(spriteBatch);
                            carChoiceSelected[0].DrawSelected(spriteBatch);
                        }

                        if (confirmedCar == "1")
                        {
                            carInfo[1].Draw(spriteBatch);
                            carChoiceSelected[1].DrawSelected(spriteBatch);
                        }

                        if (confirmedCar == "2")
                        {
                            carInfo[2].Draw(spriteBatch);
                            carChoiceSelected[2].DrawSelected(spriteBatch);
                        }

                        if (confirmedCar == "3")
                        {
                            carInfo[3].Draw(spriteBatch);
                            carChoiceSelected[3].DrawSelected(spriteBatch);
                        }

                        if (confirmedCar == "4")
                        {
                            carInfo[4].Draw(spriteBatch);
                            carChoiceSelected[4].DrawSelected(spriteBatch);
                        }

                        if (confirmedCar == "5")
                        {
                            carInfo[5].Draw(spriteBatch);
                            carChoiceSelected[5].DrawSelected(spriteBatch);
                        }

                        if (confirmedCar == "6")
                        {
                            carInfo[6].Draw(spriteBatch);
                            carChoiceSelected[6].DrawSelected(spriteBatch);
                        }

                        if (confirmedCar == "7")
                        {
                            carInfo[7].Draw(spriteBatch);
                            carChoiceSelected[7].DrawSelected(spriteBatch);
                        }

                        if (confirmedCar == "8")
                        {
                            carInfo[8].Draw(spriteBatch);
                            carChoiceSelected[8].DrawSelected(spriteBatch);
                        }

                        if (confirmedCar == "9")
                        {
                            carInfo[9].Draw(spriteBatch);
                            carChoiceSelected[9].DrawSelected(spriteBatch);
                        }

                        if (confimMouseOver)
                            confirmCarSelectedButton.DrawSelected(spriteBatch);
                        else
                            confirmCarButton.Draw(spriteBatch);


                        #endregion


                        GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                        break;
                    }

                case ScreenState.SettingsMains:
                    {
                        bloom.BeginDraw();

                        #region MainSettings_Draw

                        Background_Settings.DrawBack(spriteBatch);

                        if (SelectedSetting[0])
                            AudioButtonSelected.Draw(spriteBatch);
                        else
                            AudioButton.Draw(spriteBatch);

                        if (SelectedSetting[1])
                            VideoButtonSelected.Draw(spriteBatch);
                        else
                            VideoButton.Draw(spriteBatch);

                        if (SelectedSetting[2])
                            ControlsButtonSelected.Draw(spriteBatch);
                        else 
                            ControlsButton.Draw(spriteBatch);

                        #endregion

                        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                        break;
                    }

                case ScreenState.SettingsAudio:
                    {
                        bloom.BeginDraw();

                        #region Audio Draw

                        Background_Audio.DrawBack(spriteBatch);
                        BackMusic.Draw(spriteBatch);
                        EngineNoise.Draw(spriteBatch);
                        EnvSounds.Draw(spriteBatch);

                        #endregion

                        #region Audio_Switches Draw

                        if (audioStat[0])
                        {
                            BackMusicOFF.Draw(spriteBatch);
                            BackMusicON_Sel.Draw(spriteBatch);
                        }
                        else
                        {
                            BackMusicON.Draw(spriteBatch);
                            BackMusicOFF_Sel.Draw(spriteBatch);
                        }

                        if (audioStat[1])
                        {
                            EngineNoiseON_Sel.Draw(spriteBatch);
                            EngineNoiseOFF.Draw(spriteBatch);
                        }
                        else
                        {
                            EngineNoiseON.Draw(spriteBatch);
                            EngineNoiseOFF_Sel.Draw(spriteBatch);
                        }

                        if (audioStat[2])
                        {
                            EnvSoundON_Sel.Draw(spriteBatch);
                            EnvSoundOFF.Draw(spriteBatch);
                        }
                        else
                        {
                            EnvSoundON.Draw(spriteBatch);
                            EnvSoundOFF_Sel.Draw(spriteBatch);
                        }

                        #endregion


                        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                        break;
                    }

                case ScreenState.SettingsVideo:
                    {
                        bloom.BeginDraw();

                        #region drawSettings

                        if (ResEditMode)
                        {
                            Background_Video_Res.DrawBack(spriteBatch);

                            if (SelectedResolutionTab[0])
                                Res768_Sel.Draw(spriteBatch);
                            else
                                Res768.Draw(spriteBatch);

                            if(SelectedResolutionTab[1])
                                Res720_Sel.Draw(spriteBatch);
                            else
                                Res720.Draw(spriteBatch);

                            if (SelectedResolutionTab[2])
                                Res1080_Sel.Draw(spriteBatch);
                            else
                                Res1080.Draw(spriteBatch);

                            if(SelectedResolutionTab[3])
                                Res1440_Sel.Draw(spriteBatch);
                            else
                                Res1440.Draw(spriteBatch);
                            
                            
                            
                            
                        }
                        else
                        {
                            Background_Video.DrawBack(spriteBatch);

                            PostProcessing.Draw(spriteBatch);
                            ParticleSystem.Draw(spriteBatch);
                            MotionBlur.Draw(spriteBatch);
                            Resolution.Draw(spriteBatch);

                            if (videoStat[0])
                            {
                                PostProcessingOFF.Draw(spriteBatch);
                                PostProcessingON_Sel.Draw(spriteBatch);
                            }
                            else
                            {
                                PostProcessingON.Draw(spriteBatch);
                                PostProcessingOFF_Sel.Draw(spriteBatch);
                            }

                            if (videoStat[1])
                            {
                                ParticleSystemON_Sel.Draw(spriteBatch);
                                ParticleSystemOFF.Draw(spriteBatch);
                            }
                            else
                            {
                                ParticleSystemON.Draw(spriteBatch);
                                ParticleSystemOFF_Sel.Draw(spriteBatch);
                            }

                            if (videoStat[2])
                            {
                                MotionBlurON_Sel.Draw(spriteBatch);
                                MotionBlurOFF.Draw(spriteBatch);
                            }
                            else
                            {
                                MotionBlurON.Draw(spriteBatch);
                                MotionBlurOFF_Sel.Draw(spriteBatch);
                            }

                        }


                        #endregion

                        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                        break;
                    }

                case ScreenState.SettingsControls:
                    {
                        bloom.BeginDraw();

                        #region Control Settings Draw

                        Background_Controls.DrawBack(spriteBatch);

                        if (selectedControl[0])
                            option1_Sel.Draw(spriteBatch);
                        else
                            option1.Draw(spriteBatch);

                        if (selectedControl[1])
                            option2_Sel.Draw(spriteBatch);
                        else
                            option2.Draw(spriteBatch);

                        #endregion

                        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                        break;
                    }

                case ScreenState.Credits:
                    {
                        bloom.BeginDraw();
                        CreditsScreen.DrawBack(spriteBatch);
                        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                        break;
                    }

                case ScreenState.Map1:
                    {
                        bloom.BeginDraw();

                        //overlay
                        crosshair.DrawSelected(spriteBatch);
                        overlayFrame.DrawSelected(spriteBatch);

                        //Lasitha write your codes here to draw Map1 Contents

                        road1.Draw(this.camera);
                        mud1.Draw(this.camera);

                        bcar1.Draw(this.camera);
                        bcar2.Draw(this.camera);
                        bcar3.Draw(this.camera);
                        bcar4.Draw(this.camera);
                        bcar5.Draw(this.camera);

                        for (int i = 0; i < wall_right.Length; i++)
                        {
                            wall_right[i].Draw(this.camera);
                            wall_left[i].Draw(this.camera);
                            wall_up[i].Draw(this.camera);
                            wall_down[i].Draw(this.camera);
                        }

                        //junk tyres
                        tire_in1.Draw(this.camera);
                        tire_in2.Draw(this.camera);
                        tire_in3.Draw(this.camera);

                        tire_out1.Draw(this.camera);
                        tire_out2.Draw(this.camera);
                        tire_out3.Draw(this.camera);
                        tire_out4.Draw(this.camera);

                        //panes

                        panes[0].Draw(this.camera);
                        panes[1].Draw(this.camera);
                        panes[2].Draw(this.camera);
                        panes[3].Draw(this.camera);

                        //skydome

                      //  skyDome.Draw(this.camera);

                        //speedometer

                        if (health > 80)
                            speedoMeters[0].DrawSelected(spriteBatch);
                        if (health > 30 && health < 81)
                            speedoMeters[1].DrawSelected(spriteBatch);
                        if (health < 31)
                            speedoMeters[2].DrawSelected(spriteBatch);

                        //spriteBatch.DrawString(speedMeter, health.ToString() + "%", new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 1.08f)), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(speedMeter, meterSpeed.ToString(), new Vector2((Window.ClientBounds.Width / 13), (Window.ClientBounds.Height / 1.25f)), Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(speedMeter, meterRpm.ToString(), new Vector2((Window.ClientBounds.Width / 11), (Window.ClientBounds.Height / 1.15f)), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);

                        spriteBatch.DrawString(speedMeter, health.ToString() + "%", new Vector2((Window.ClientBounds.Width / 1.2f), (Window.ClientBounds.Height / 1.08f)), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                        spriteBatch.DrawString(speedMeter, ((int)meterSpeed).ToString(), new Vector2((Window.ClientBounds.Width / 1.25f), (Window.ClientBounds.Height / 1.25f)), Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1);
                        spriteBatch.DrawString(speedMeter, ((int)meterRpm).ToString(), new Vector2((Window.ClientBounds.Width / 1.235f), (Window.ClientBounds.Height / 1.15f)), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);

                        //spriteBatch.DrawString(myFont, "sm : " + sm.getPosition() , new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 10)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(myFont, "car : " + vr.getWorldLoc().X.ToString() + "   " + vr.getWorldLoc().Y.ToString() + "   " +  vr.getWorldLoc().Z.ToString(), new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 8)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                        //spriteBatch.DrawString(myFont, "X : " + camera.returnPosX() + " Y : " + camera.returnPosY() + " Z : " + camera.returnPosZ(), new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 10)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(myFont, "Pitch : " + camera.returnPitch() + " Roll : " + camera.returnRoll() + " Yaw : " + camera.returnYaw(), new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 7)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(myFont, "XX : " + cars[0].getPosition().X + " YY : " + cars[0].getPosition().Y + " ZZ : " + cars[0].getPosition().Z, new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 5)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(myFont, "netX : " + xx + "netY : " + yy + "netXX : " + xx1 + "netYY : " + yy1, new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 2)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                       
                        //prasad

                        vr.Draw(this.camera);

                        camera.changeCameraPos(vr.getWorldLoc(), vr.getQuaRot(), device);

                        GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                            break;
                    }

                case ScreenState.Map2:
                    {
                        bloom.BeginDraw();

                        //overlay
                        overlayFrame.DrawSelected(spriteBatch);
                        crosshair.DrawSelected(spriteBatch);

                        //floors

                        for (int i = 0; i < FloorMap2.Length; i++)
                        {
                            FloorMap2[i].Draw(this.camera);
                        }

                        //walls

                        for (int i = 0; i < WallMap2.Length; i++)
                        {
                            WallMap2[i].Draw(this.camera);
                        }

                        //trees

                        for (int i = 0; i < TreeMap2.Length; i++)
                        {
                           TreeMap2[i].Draw(this.camera);
                        }

                        //panes

                        for (int i = 0; i < PaneMap2.Length; i++)
                        {
                            PaneMap2[i].Draw(this.camera);
                        }

                        //junk tires

                        for (int i = 0; i < JunkTireMap2.Length; i++)
                        {
                            JunkTireMap2[i].Draw(this.camera);
                        }

                        //broken cars

                        for (int i = 0; i < BrokenCarMap2.Length; i++)
                        {
                            BrokenCarMap2[i].Draw(this.camera);
                        }
                        

                        //spriteBatch.DrawString(myFont, "sm : " + sm.getPosition() , new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 10)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(myFont, "car : " + vr.getWorldLoc().X.ToString() + "   " + vr.getWorldLoc().Y.ToString() + "   " + vr.getWorldLoc().Z.ToString(), new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 8)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                        //spriteBatch.DrawString(myFont, "X : " + camera.returnPosX() + " Y : " + camera.returnPosY() + " Z : " + camera.returnPosZ(), new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 10)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(myFont, "Pitch : " + camera.returnPitch() + " Roll : " + camera.returnRoll() + " Yaw : " + camera.returnYaw(), new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 7)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(myFont, "XX : " + cars[0].getPosition().X + " YY : " + cars[0].getPosition().Y + " ZZ : " + cars[0].getPosition().Z, new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 5)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(myFont, "netX : " + xx + "netY : " + yy + "netXX : " + xx1 + "netYY : " + yy1, new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 2)), Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                        //prasad

                        vr.Draw(this.camera);
                        //tr1.Draw(this.camera);
                        //tr2.Draw(this.camera);
                        //tl1.Draw(this.camera);
                        //tl2.Draw(this.camera);


                        //speedometer

                        if(health>80)
                            speedoMeters[0].DrawSelected(spriteBatch);
                        if(health>30 && health<81)
                            speedoMeters[1].DrawSelected(spriteBatch);
                        if(health<31)
                            speedoMeters[2].DrawSelected(spriteBatch);

                        //spriteBatch.DrawString(speedMeter, health.ToString()+"%", new Vector2((Window.ClientBounds.Width / 10), (Window.ClientBounds.Height / 1.08f)), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(speedMeter, meterSpeed.ToString(), new Vector2((Window.ClientBounds.Width / 13), (Window.ClientBounds.Height / 1.25f)), Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1);
                        //spriteBatch.DrawString(speedMeter, meterRpm.ToString(), new Vector2((Window.ClientBounds.Width / 11), (Window.ClientBounds.Height / 1.15f)), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);

                        spriteBatch.DrawString(speedMeter, health.ToString() + "%", new Vector2((Window.ClientBounds.Width / 1.2f), (Window.ClientBounds.Height / 1.08f)), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                        spriteBatch.DrawString(speedMeter, ((int)meterSpeed).ToString(), new Vector2((Window.ClientBounds.Width / 1.25f), (Window.ClientBounds.Height / 1.25f)), Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1);
                        spriteBatch.DrawString(speedMeter, ((int)meterRpm).ToString(), new Vector2((Window.ClientBounds.Width / 1.235f), (Window.ClientBounds.Height / 1.15f)), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);

                        camera.changeCameraPos(vr.getWorldLoc(), vr.getQuaRot(), device);

                        GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                        break;
                    }


            }
            spriteBatch.End();
         
            base.Draw(gameTime);
        }


        #region OtherMethodsStartHere

        #region MainMenu_Methods

        public void Links_MainMenu()
        {
            if (mouseRect.Intersects(btn_StartServer.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                currentState = ScreenState.CreateServer;
            }

            if (mouseRect.Intersects(btn_JoinGame.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                currentState = ScreenState.JoinGame;
            }

            if (mouseRect.Intersects(btn_Profile.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                #region UpdateProfileNames
                const string f1 = "Profiles.txt";
                StreamReader r1 = new StreamReader(f1);
                string line1;
                line1 = r1.ReadLine();
                users = line1.Split('#');
                profileNames = users[0].Split('@');
                carNames = users[1].Split('@');
                r1.Close();
                #endregion
                currentState = ScreenState.Profile;
            }

            if (mouseRect.Intersects(btn_Customize.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                currentState = ScreenState.Customize;
            }

            if (mouseRect.Intersects(btn_Settings.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                currentState = ScreenState.SettingsMains;
            }

            if (mouseRect.Intersects(btn_Help.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                Process.Start("IExplore.exe", "http://www.facebook.com/EleanoraGames");
            }

            if (mouseRect.Intersects(btn_Credits.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                currentState = ScreenState.Credits;
            }

            if (mouseRect.Intersects(btn_Quit.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                this.Exit();
            }
            
        }

        #endregion

        #region CreateServer_Methods

        public void updateMapSelection()
        {
            if (mouseRect.Intersects(btn_map1.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                map1_bool = true;
                map2_bool = false;
                map3_bool = false;
            }

            if (mouseRect.Intersects(btn_map2.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                map1_bool = false;
                map2_bool = true;
                map3_bool = false;
            }

            if (mouseRect.Intersects(btn_map3.getRectangle()) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MenuItemSelect.Play();
                map1_bool = false;
                map2_bool = false;
                map3_bool = true;
            }
        }

        public void updatePlayerCountSelection()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1))
            {
                MenuItemSelect.Play();
                for (int x = 0; x < 8; x++)
                {
                    playerCountState[x] = false;
                }

                playerCountState[0] = true;
                playerCount = 1;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
            {
                MenuItemSelect.Play();
                for (int x = 0; x < 8; x++)
                {
                    playerCountState[x] = false;
                }

                playerCountState[1] = true;
                playerCount = 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad3))
            {
                MenuItemSelect.Play();
                for (int x = 0; x < 8; x++)
                {
                    playerCountState[x] = false;
                }

                playerCountState[2] = true;
                playerCount = 3;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
            {
                MenuItemSelect.Play();
                for (int x = 0; x < 8; x++)
                {
                    playerCountState[x] = false;
                }

                playerCountState[3] = true;
                playerCount = 4;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad5))
            {
                MenuItemSelect.Play();
                for (int x = 0; x < 8; x++)
                {
                    playerCountState[x] = false;
                }

                playerCountState[4] = true;
                playerCount = 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad6))
            {
                MenuItemSelect.Play();
                for (int x = 0; x < 8; x++)
                {
                    playerCountState[x] = false;
                }

                playerCountState[5] = true;
                playerCount = 6;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad7))
            {
                MenuItemSelect.Play();
                for (int x = 0; x < 8; x++)
                {
                    playerCountState[x] = false;
                }

                playerCountState[6] = true;
                playerCount = 7;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad8))
            {
                MenuItemSelect.Play();
                for (int x = 0; x < 8; x++)
                {
                    playerCountState[x] = false;
                }

                playerCountState[7] = true;
                playerCount = 8;
            }
        }

        #endregion

        #region Collisions and Particles

        public bool collide()
        {
             
            //foreach (ModelMesh my in cars[0].model.Meshes)
            //{
            //    foreach (ModelMesh his in s2.model.Meshes)
            //    {
                    

            //        if (my.BoundingSphere.Transform(cars[0].getMyWorld()).Intersects(his.BoundingSphere.Transform(s2.getMyWorld())))
            //        {
            //            return true;
            //        }
            //    }
            //}

            return false;
        }


        // Particle Methods

        public void UpdateExplosions(GameTime gameTime)
        {
            //loop through and update explisions

            for (int i = 0; i < explosions.Count; ++i)
            {
                explosions[i].UpdateParticles(gameTime);

                //if explosion is finished remove that bitch :D

                if (explosions[i].isDead())
                {
                    explosions.RemoveAt(i);
                }


            }
        }

        #endregion

        #endregion
    }
}
