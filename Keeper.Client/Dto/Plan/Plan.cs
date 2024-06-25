namespace Keeper.Client;

public partial class Plan : BaseDto
{
    public int Id { get; set; }
        
    public string Name { get; set; } = null!;
        
    public decimal Price { get; set; }

    public int Duration { get; set; }

    public PlanType Type { get; set; }
}