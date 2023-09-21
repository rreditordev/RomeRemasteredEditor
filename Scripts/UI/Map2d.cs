using Controller;
using Godot;
using Model;
using RtwFileIO;
using System.Linq;
using UIUtilities;

public partial class Map2d : MeshInstance2D
{

    float pixelsPerUnit = 100f;
    public float ratio;
    public int widthScale;
    public int heightScale;
    public int gameWidth;
    public int gameHeight;

    [Export]
    public TileSelector tileSelector;

    [Export]
    public Node2D root;
    [Export]
    public SaveDataPathBtn SaveDataPath;
    [Export]
    public Control StartUpPanel;

    [ExportGroup("MapItems")]
    [Export]
    public PackedScene resourceScene;
    [Export]
    public PackedScene landmarkScene;
    [Export]
    public PackedScene characterScene;


    Node2D resourceContainer;
    Node2D landmarkContainer;

    // Called when the node enters the scene tree for the first time.
    public override async void _Ready() {
        //Check if perfs.json path is valid and if not ask for a replacement
        var response = Preferences.SetPath(Preferences.CurrentDataPath());
        if (!response.result) {
            StartUpPanel.Visible = true;
            await ToSignal(SaveDataPath, SaveDataPathBtn.SignalName.CorrectDataPath);
        } else {
            StartUpPanel.Visible = false;
        }
        RtwDataContext.Load(Preferences.CurrentDataPath());           


        var image = Image.LoadFromFile(DataFilesPaths.MapRegionsTgaPath);
        image.FlipY();
        gameWidth = image.GetWidth();
        gameHeight = image.GetHeight();
        var texture = ImageTexture.CreateFromImage(image);
        Texture = texture;

        ratio = (image.GetWidth() / pixelsPerUnit) / (image.GetHeight() / pixelsPerUnit);
        Scale = new Vector2(ratio * Scale.X, Scale.Y);
        widthScale = (int)Scale.X/2;
        heightScale = (int)Scale.Y/2;

        DisplayResources();
        DisplayLandmarks();
        DisplayCharacters();
    }

    public void DisplayResources(bool resourceTexMissing = false) {
        if (resourceContainer == null) {
            resourceContainer = new Node2D();
            resourceContainer.Name = "ResourceContainer";
            root.CallDeferred("add_child", resourceContainer);
        } else {
            var childern = resourceContainer.GetChildren();
            foreach (var child in childern) {
                child.Free();
            }
        }

        foreach (var res in RtwDataContext.Campaign.GetResources()) {
            var resSprite = resourceScene.Instantiate<Sprite2D>();
            var resObjLoc = UnitConversions.GameToWorldCoordinates(gameHeight, gameWidth, (int)res.MapPosition.X, res.MapPosition.Y, Scale);
            resSprite.Position = resObjLoc;
            var resScript = resSprite as Resource;
            int num = 0;
            foreach(var tex in resScript.resTexArray) {
                if(tex.ResourcePath.Contains(res.ResourceID)) {
                    resSprite.Texture = tex;
                } else {
                    num++;
                }
            }
            if (resourceTexMissing && num == resScript.resTexArray.Length) {
                GD.Print($"Missing Textures: {res.ResourceID}");
            }
            resSprite.Name = res.ResourceID;
            resourceContainer.AddChild(resSprite);

        }
    }

    public void DisplayLandmarks() {
        var landmarkContainer = new Node2D();
        landmarkContainer.Name = "LandmarkContainer";
        root.CallDeferred("add_child", landmarkContainer);
        foreach (var landmark in RtwDataContext.Campaign.GetLandmarks()) {
            var landmarkSprite = landmarkScene.Instantiate<Sprite2D>();
            var landmarkObjLoc = UnitConversions.GameToWorldCoordinates(gameHeight, gameWidth, (int)landmark.MapPosition.X, landmark.MapPosition.Y, Scale);
            landmarkSprite.Position = landmarkObjLoc;
            landmarkSprite.Name = landmark.ID;
            landmarkContainer.AddChild(landmarkSprite);
        }
    }

    public void DisplayCharacters() {
        var charContainer = new Node2D();
        charContainer.Name = "ResourceContainer";
        root.CallDeferred("add_child", charContainer);

        foreach (var character in CampaignCharactersController.GetCharacters()) {
            var charSprite = characterScene.Instantiate<Sprite2D>();
            var resObjLoc = UnitConversions.GameToWorldCoordinates(gameHeight, gameWidth, (int)character.MapPosition.X, character.MapPosition.Y, Scale);
            charSprite.Position = resObjLoc;
            var charScript = charSprite as character;
            foreach (var tex in charScript.resTexArray) {
                if (tex.ResourcePath.Contains(character.Type.ToString())) {
                    charSprite.Texture = tex;
                } 
            }
            charSprite.Name = $"{character.Name}: {character.Type}";
            charContainer.AddChild(charSprite);

        }
    }

    public override void _UnhandledInput(InputEvent @event) {
        if (@event is InputEventMouseButton mb) {
            if (mb.ButtonIndex == MouseButton.Left && !mb.Pressed) {
                tileSelector.CreateIndicator(this);
            }
        }
    }
}
