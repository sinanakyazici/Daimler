using System;

namespace Daimler.Lib.Logger
{
    public interface IDaimlerLogger
    {
        void Information(string text);
        void Error(string text);
        void Error(Exception exception, string text);
        void Warning(string text);
        void Warning(Exception ex, string text);
    }
}