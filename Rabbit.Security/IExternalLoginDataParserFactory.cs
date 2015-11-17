namespace Rabbit.Security
{
    public interface IExternalLoginDataParserFactory
    {
        IExternalLoginDataParser Create(string provider);
    }
}