using DataAccess.Abstract;
using Entities.Models;
using NLog;

namespace DataAccess.Concrete
{
    public class LoggerManagerRepository: ILoggerManagerRepository
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private DataContext _context;

        public LoggerManagerRepository(DataContext context)
        {
            _context = context;
        }
        public void Debug(int statusCode, string message)
        {
            _logger.Debug(message);
        }
        public void Info(int statusCode, string message)
        {
            _logger.Info(message);
        }

        public void Warn(int statusCode, string message)
        {
            _logger.Warn(message);
        }

        public void Error(int statusCode, string message)
        {
            _logger.Warn(message);
        }

        public void Save(int statusCode, string message)
        {
            Log log = new Log();
            log.StatusCode = statusCode;
            log.Message = message;
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

    }
}
