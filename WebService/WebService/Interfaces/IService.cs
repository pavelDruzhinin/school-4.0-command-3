﻿using System.Threading.Tasks;

namespace WebService
{
    public interface IService
    {
        string Uri { get; }
        void Run();
        Task HandleRequest(string requestContent);
    }
}