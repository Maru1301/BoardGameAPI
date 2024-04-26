using AutoMapper;

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

        public static T ToVM<T>(this object dto) where T : new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(dto.GetType(), typeof(T));
            });
            IMapper mapper = config.CreateMapper();
            var vm = mapper.Map<T>(dto);
            return vm;
        }

        public static T ToEntity<T>(this object dto) where T : new()
        {
            var config = new MapperConfiguration(cfg =>
            { 
                cfg.CreateMap(dto.GetType(), typeof(T)); 
            });
            IMapper mapper = config.CreateMapper();
            var entity = mapper.Map<T>(dto);
            return entity;
        }
    }
}
