using DemoGlobalExceptionHandling.Api.Helpers;
using System;

namespace DemoGlobalExceptionHandling.Api.Data
{
    public class FakeData
    {
        public int GetRandomNumber()
        {
            ExceptionHelper.RandomlyThrowException();

            return new Random().Next();
        }
    }
}