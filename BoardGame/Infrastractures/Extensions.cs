using AutoMapper;
using BoardGame.Models.DTOs;

namespace BoardGame.Infrastractures
{
    public static class Extensions
    {
        public static T ToDTO<T>(this object vm) where T : new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(vm.GetType(), typeof(T));
            });
            IMapper mapper = config.CreateMapper();
            var dto = mapper.Map<T>(vm);
            return dto;
        }

        public static T ToVM<T>(this object entity) where T : new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(entity.GetType(), typeof(T));
            });
            IMapper mapper = config.CreateMapper();
            var dto = mapper.Map<T>(entity);
            return dto;
        }
    }
}
