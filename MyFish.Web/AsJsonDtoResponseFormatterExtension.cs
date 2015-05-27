using Nancy;

namespace MyFish.Web
{
    public static class AsJsonDtoResponseFormatterExtension
    {
        public static Response AsJsonDto<TModel>(this IResponseFormatter formatter, TModel model)
        {
            var dto = AutoMapper.Map(model);

            return formatter.AsJson(dto);
        }
    }
}