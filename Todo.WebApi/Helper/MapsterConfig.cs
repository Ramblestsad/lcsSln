using Mapster;

namespace Todo.WebApi.Helper;
public static class MappingConfig
{
    public static readonly TypeAdapterConfig Config;

    static MappingConfig()
    {
        Config = new TypeAdapterConfig();

        // 示例：MapperTestFoo → MapperTestFooDto
        Config.NewConfig<MapperTestFoo, MapperTestFooDto>()
            .Map(dest => dest.PriceDisplay, src => $"￥{src.Price:F2}")
            .Map(dest => dest.Name, src => src.Name);
    }
}

public class MapperTestFoo
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
}

public class MapperTestFooDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal PriceDisplay { get; set; }
}
