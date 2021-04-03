using System;

namespace Aciderp.CQRS.Command
{
    public class CommandHandlerNotFoundException
        : Exception
    {
        public CommandHandlerNotFoundException(string missingCommanHandlerType)
        : base(FormatErrorMessage(missingCommanHandlerType)) { }

        private static string FormatErrorMessage(string missingCommanHandlerType)
        {
            var message =
                $@"There is no command handler of type:
                {missingCommanHandlerType} registered";

            return message;
        }
    }
}
