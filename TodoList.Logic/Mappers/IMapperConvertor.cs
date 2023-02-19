namespace TodoList.Logic.Mappers
{
    public interface IMapperConvertor<TFrom, TTo>
    {
        TTo Convert(TFrom from);
    }
}
