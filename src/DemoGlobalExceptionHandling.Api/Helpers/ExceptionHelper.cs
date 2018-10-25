using System;

namespace DemoGlobalExceptionHandling.Api.Helpers
{
    public static class ExceptionHelper
    {
        public static void RandomlyThrowException()
        {
            bool IsOddRandomNumber() => new Random().Next(0, 1000) % 2 != 0;

            if (IsOddRandomNumber())
            {
                throw new InvalidOperationException("Randomly generated exception");
            }
        }
    }
}