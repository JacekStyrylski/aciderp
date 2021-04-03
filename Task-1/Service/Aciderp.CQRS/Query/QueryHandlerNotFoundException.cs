using System;

namespace Aciderp.CQRS.Query
{
    public class QueryHandlerNotFoundException
        : Exception
    {
        public QueryHandlerNotFoundException(string missingHandlerType)
        : base(FormatErrorMessage(missingHandlerType)) { }

        private static string FormatErrorMessage(string missingHandlerType)
        {
            var message =
                $@"There is no query handler of type:
                {missingHandlerType} registered";

            return message;
        }
    }
}
