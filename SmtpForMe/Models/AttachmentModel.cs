namespace SmtpForMe.Models
{
    public class AttachmentModel
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
