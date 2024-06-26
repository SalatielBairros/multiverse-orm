﻿
using Dapper.Fluent.Application;
using Dapper.Fluent.Domain;
using Multiverse.Mapping;

namespace Dapper.Fluent.Repository.FluentMapper;

public class PublicSchemaEntityMap : DapperFluentEntityMap<PublicSchemaEntity>
{
    public PublicSchemaEntityMap(string schema)
        : base(schema)
    {
        ToTable("sampleentity", schema);
        WithEntityValidation();
        MapToColumn(x => x.Id).IsKey().IsIdentity();
        MapToColumn(x => x.IntProperty).Default(5).NotNull();
        MapToColumn(x => x.LimitedTextProperty).WithLenght(255);
        MapToColumn(x => x.TextProperty).NotNull();
        Map(x => x.DateProperty).ToColumn("datepp");
        Map(x => x.DecimalProperty).ToColumn("decimalpp");
        MapToColumn(x => x.BooleanProperty);
        MapToColumn(x => x.CategoryId).ForeignKeyFor<Category>("id");
    }
}
