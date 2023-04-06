using System;

namespace SalesWebMVC.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // retorna se RequestId é nulo ou não
    }
}