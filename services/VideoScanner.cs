using VideoLibraryServer.Models;
using System.IO;

namespace VideoLibraryServer.services
{
    public class VideoScanner
    {
        private readonly string[] _extensions = { ".mp4", ".mkv", ".avi", ".mov", ".webm" };

        public List<VideoFile> ScanFolders(IEnumerable<string> folders)
        {
            var videos = new List<VideoFile>();
            foreach (var folder in folders)
            {
                if (!Directory.Exists(folder)) continue;

                var files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)
                                     .Where(f => _extensions.Contains(Path.GetExtension(f).ToLower()));

                foreach (var file in files)
                {
                    var info = new FileInfo(file);
                    videos.Add(new VideoFile
                    {
                        FileName = info.Name,
                        FilePath = file.Replace(folder, "").Replace("\\", "/"), // relative path
                        FileSize = info.Length,
                        LastModified = info.LastWriteTime
                    });
                }
            }
            return videos;
        }
    }
}
