using GildedRoseKata;

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

namespace GildedRoseTests;

public class ApprovalTest
{
    [Fact]
    public void UpdateQuality_WhenCalledWithAgedBrie_IncreasesQualityWhileSellInApproachesZero()
    {
        var items = new[] { new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 } };
        
        var system = new GildedRose(items);
        
        system.UpdateQuality();
        
        Assert.Equal(1, items[0].SellIn);
        Assert.Equal(1, items[0].Quality);
        
        system.UpdateQuality();
        
        Assert.Equal(0, items[0].SellIn);
        Assert.Equal(2, items[0].Quality);
    }
    
    [Fact]
    public void UpdateQuality_WhenCalledWithSulfuras_KeepsQualityAndSellIn()
    {
        var items = new[] { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } };
        
        var system = new GildedRose(items);
        
        system.UpdateQuality();
        
        Assert.Equal(0, items[0].SellIn);
        Assert.Equal(80, items[0].Quality);
    }
    
    [Fact]
    public void UpdateQuality_WhenCalledWithBackstagePasses_IncreasesQualityWhenApproachingSellInDayWithA50QualityCap()
    {
        var items = new[] { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 2, Quality = 49 } };
        
        var system = new GildedRose(items);
        
        system.UpdateQuality();
        
        Assert.Equal(1, items[0].SellIn);
        Assert.Equal(50, items[0].Quality);
        
        system.UpdateQuality();
        
        Assert.Equal(0, items[0].SellIn);
        Assert.Equal(50, items[0].Quality);
    }
}