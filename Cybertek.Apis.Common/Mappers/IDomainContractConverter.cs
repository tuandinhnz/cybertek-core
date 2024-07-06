namespace Cybertek.Apis.Common.Mappers;

public interface IDomainContractConverter
{
    object Convert(Type targetType, object source);
}
