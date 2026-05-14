using System.Collections.Generic;
using System.IO;
using System.Text;
using Claritty.Src;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Claritty;

public class Terminal : Game
{
    public static Terminal Instance = null!;

    private static GraphicsDeviceManager _graphics = null!;
    private static SpriteBatch _spriteBatch = null!;

    private static readonly FontSystem _FontSystem = new();
    private static SpriteFontBase _font = null!;

    public static readonly List<string> OutHist = new(128);
    public static readonly StringBuilder CurLine = new(128);
    public static readonly ShellProcess Shell = new();

    public const string BufferName = "ClarittyGraphics";
    public static readonly SharedGraphicsBuffer Buffer = new(BufferName);

    public Terminal()
    {
        Instance = this;
        _graphics = new GraphicsDeviceManager(this);
        this.IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        _spriteBatch = new SpriteBatch(this.GraphicsDevice);
        _FontSystem.AddFont(File.ReadAllBytes("Claritty/lilex.ttf"));
        _font = _FontSystem.GetFont(20);

        this.Window.TextInput += InputLib.TextInput;
        Buffer.CreateOrResize(1920, 1080);
        Shell.Start();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(this.GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        // InputLib.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        _spriteBatch.DrawString(_font, CurLine,
            new Vector2(0, 460), Color.White);

        _spriteBatch.DrawString(_font, string.Join(null, OutHist),
            new Vector2(0, 0), Color.White);

        Buffer.UpdateTexture();
        _spriteBatch.Draw(Buffer.Texture, Vector2.Zero, null, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
