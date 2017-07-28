using KryAIO.Logger;

namespace KryAIO
{
    public interface ILogger
    {
        void Log(string message, LogType logType, EventType eventType);
    }
}