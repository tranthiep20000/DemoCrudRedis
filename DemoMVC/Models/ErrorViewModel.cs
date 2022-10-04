namespace DemoMVC.Models
{
    public class ErrorViewModel
    {
        #region Property

        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        #endregion Property
    }
}