﻿using Dapper.Fluent.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dapper.Fluent.Application;

public interface IBigDataRepository
{
    void InsertList(IEnumerable<BigData> data);
    Task InsertListAsync(IEnumerable<BigData> data);
}