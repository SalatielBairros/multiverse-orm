﻿
namespace Multiverse.Contracts;

public interface IRepositorySettings
{
    string ConnString { get; }
    string Schema { get; }
    string DefaultSchema { get; }    
}
