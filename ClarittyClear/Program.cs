using System.IO.MemoryMappedFiles;

public sealed class Program
{
    public static void Main(string[] args)
    {
        string path = $"/dev/shm/ClarittyGraphics";

        int width = 1920;
        int height = 1080;
        long size = (long) width * height * 4;
        
        using MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(path, FileMode.Open, null,
            size, MemoryMappedFileAccess.ReadWrite);
        using MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor();

        byte[] pixels = new byte[size];

        accessor.WriteArray(0, pixels, 0, pixels.Length);
    }
}
