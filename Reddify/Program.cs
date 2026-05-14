using System.IO.MemoryMappedFiles;

public class Program
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

        for (int i = 0; i < pixels.Length; i += 4)
        {
            pixels[i + 0] = 255;
            pixels[i + 1] = 0;
            pixels[i + 2] = 0;
            pixels[i + 3] = 255;
        }

        accessor.WriteArray(0, pixels, 0, pixels.Length);

        Console.WriteLine("youre red now! thats my attack!");
    }
}
