namespace WebAdmin.Models
{
    /// <summary>
    ///  Add Comment that only display on Staging branch
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        /// <summary>
        /// More note after Class comment above, Expect that code merged will exclude Class comment
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}