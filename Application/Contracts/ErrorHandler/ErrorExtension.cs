namespace Application.Contracts {
    public abstract class ErrorExtension {
        public string Instance { get; set; }
        public IDictionary<string, object> Extensions { get; set; }
    }
}