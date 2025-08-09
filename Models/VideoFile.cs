namespace VideoLibraryServer.Models
{
    public class VideoFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; } // relative path for streaming
        public long FileSize { get; set; }
        public DateTime LastModified { get; set; }
    }
}
