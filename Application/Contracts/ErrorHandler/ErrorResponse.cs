namespace Application.Contracts {
    public class ErrorResponse : ErrorExtension {
        public ErrorCode Code { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }

        public ErrorResponse(string message, string description, ErrorCode code = ErrorCode.InvalidOperation) {
            Message = message;
            Description = description;
            Code = code;
            Instance = string.Empty;
            Extensions = new Dictionary<string, object>();
        }

        public ErrorResponse() {
            Message = string.Empty;
            Description = string.Empty;
            Code = ErrorCode.InvalidOperation;
            Instance = string.Empty;
            Extensions = new Dictionary<string,object>();
        }

    }
}