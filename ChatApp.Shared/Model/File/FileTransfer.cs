using Newtonsoft.Json;

namespace ChatApp.Shared.Model.File
{
    public class FileTransfer
    {
        public FileTransfer()
        {
        }

        public FileTransfer(Guid senderUserId, Guid targetUserId, string fileName, byte[] fileContent)
        {
            Id = Guid.NewGuid();
            SenderUserId = senderUserId;
            TargetUserId = targetUserId;
            FileName = fileName;
            FileSize = fileContent.Length;
            Content = fileContent;
        }

        public Guid Id { get; init; }
        public Guid SenderUserId { get; init; }
        public Guid TargetUserId { get; init; }
        public string FileName { get; init; }
        public long FileSize { get; init; } // File size in bytes
        public byte[] Content { get; init; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static FileTransfer? FromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON cannot be null or empty.", nameof(json));

            return JsonConvert.DeserializeObject<FileTransfer>(json);
        }

        public string GetContentAsBase64()
        {
            return Convert.ToBase64String(Content);
        }

        public static FileTransfer FromBase64(Guid senderUserId, Guid targetUserId, string fileName,
            string base64Content)
        {
            if (string.IsNullOrWhiteSpace(base64Content))
                throw new ArgumentException("Base64 content cannot be null or empty.", nameof(base64Content));

            var fileContent = Convert.FromBase64String(base64Content);
            return new FileTransfer(senderUserId, targetUserId, fileName, fileContent);
        }
    }
}