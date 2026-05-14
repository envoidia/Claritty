using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Microsoft.Xna.Framework.Graphics;

namespace Claritty.Src;

public sealed class SharedGraphicsBuffer(string name)
{
    private MemoryMappedFile? _mmf;
    private MemoryMappedViewAccessor? _accessor;
    public Texture2D? Texture;
    private byte[]? _tempBuffer;
    private readonly string _path = $"/dev/shm/{name}";

    public void CreateOrResize(int width, int height)
    {
        long newSize = (long) width * height * 4;

        // Close old mapping
        this._accessor?.Dispose();
        this._mmf?.Dispose();

        using (FileStream fs = new(this._path, FileMode.OpenOrCreate,
            FileAccess.ReadWrite, FileShare.ReadWrite))
        {
            if (fs.Length != newSize)
            {
                fs.SetLength(newSize);
            }
        }

        this._mmf = MemoryMappedFile.CreateFromFile(this._path, FileMode.Open, null,
            newSize, MemoryMappedFileAccess.ReadWrite);

        this._accessor = this._mmf.CreateViewAccessor(0, newSize);

        this._tempBuffer = new byte[newSize];

        if (this.Texture is null || this.Texture.Width != width || this.Texture.Height != height)
        {
            this.Texture?.Dispose();
            this.Texture = new Texture2D(Terminal.Instance.GraphicsDevice, width, height, false,
                SurfaceFormat.Color);
        }

        Debug.Log($"Shared buffer resized to {width}x{height}");
    }

    public void UpdateTexture()
    {
        if (this._accessor is null || this.Texture == null || this._tempBuffer == null)
        {
            return;
        }

        try
        {
            this._accessor.ReadArray(0, this._tempBuffer, 0, this._tempBuffer.Length);
            this.Texture.SetData(this._tempBuffer);
        }
        catch (Exception ex)
        {
            Debug.Log($"Shared memory read failed: {ex.Message}");
        }
    }
}
