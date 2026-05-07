namespace SantasAllotment.Tests;

public class AllotmentTests
{
    [Fact]
    public void PlaceTrees_ValidNumberOfTrees_ReturnsCorrectNumberOfPlacements()
    {
        var allotment = new Allotment(4);
        var allotmentManager = new AllotmentManager(allotment);
        
        var result = allotmentManager.ProcessAllotment();
        
        Assert.NotEmpty(result);
    }
    
    [Fact]
    public void PlaceTrees_InvalidNumberOfTrees_ReturnsEmptyResult()
    {
        var allotment = new Allotment(2);
        var allotmentManager = new AllotmentManager(allotment);
        
        var result = allotmentManager.ProcessAllotment();
        
        Assert.Empty(result);
    }
}