namespace WebAdmin.Models
{
    /// <summary>
    ///  Add Comment that only display on Staging branch
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}