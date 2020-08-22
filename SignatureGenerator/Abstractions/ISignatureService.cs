namespace SignatureGenerator.Abstractions
{
    public interface ISignatureService
    {
        string Sign(string content);
    }
}