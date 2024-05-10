using AutoMapper;

namespace BoardGame.Infrastractures
{
    public static class Extensions
    {
        public static T To<T>(this object source) where T : new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(source.GetType(), typeof(T));
            });
            IMapper mapper = config.CreateMapper();
            var target = mapper.Map<T>(source);
            return target;
        }
    }
}
