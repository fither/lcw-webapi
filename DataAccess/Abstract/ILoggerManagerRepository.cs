namespace DataAccess.Abstract
{
    public interface ILoggerManagerRepository
    {
        void Info(int statusCode, string message);
        void Warn(int statusCode, string message);
        void Debug(int statusCode, string message);
        void Error(int statusCode, string message);
        void Save(int statusCode, string message);
    }
}
