namespace ControleFinanceiro.Api.Domain.Struct;

public readonly record struct IdCustomizado(Guid value)
{
    public static IdCustomizado Empty => new(Guid.Empty);
    public static IdCustomizado NewGuidCustomerId() => new(Guid.NewGuid());

    public static IdCustomizado Parse(string s) 
    {
        return new IdCustomizado(Guid.Parse(s));
    }
    
    public static bool TryParse(string s, out IdCustomizado result)
    {
        if (Guid.TryParse(s, out var guidResult))
        {
            result = new IdCustomizado(guidResult);
            return true;
        }

        result = Empty;
        return false;
    }
}