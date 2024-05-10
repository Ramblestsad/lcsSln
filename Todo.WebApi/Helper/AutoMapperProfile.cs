using AutoMapper;

namespace Todo.WebApi.Helper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile() => CreateMap<MapperTestFoo, MapperTestFooDto>();
}

public class MapperTestFoo
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class MapperTestFooDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
