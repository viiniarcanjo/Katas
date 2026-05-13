using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose(IList<Item> items)
{
    private readonly IList<Item> _items = items;

    private readonly Dictionary<string, Action<Item>> _specificItemRules = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Aged Brie"] = item =>
        {
            if (item.Quality < 50)
                item.Quality += 1;

            item.SellIn -= 1;

            if (item.SellIn < 0 && item.Quality < 50)
                item.Quality += 1;
        },
        ["Backstage passes to a TAFKAL80ETC concert"] = item =>
        {
            if (item.Quality < 50)
                item.Quality += 1;

            if (item.Quality < 50 && item.SellIn < 11)
                item.Quality += 1;

            if (item.Quality < 6 && item.SellIn < 11)
                item.Quality += 1;

            item.SellIn -= 1;

            if (item.SellIn < 0)
                item.Quality = 0;
        },
        ["Sulfuras, Hand of Ragnaros"] = item => { }
    };

    public void UpdateQuality()
    {
        for (var i = 0; i < _items.Count; i++)
            ProcessSellInAndQuality(i);
    }

    private void ProcessSellInAndQuality(int index)
    {
        var item = _items[index];

        if (!_specificItemRules.TryGetValue(item.Name, out var applyRule))
        {
            ProcessDefaultItem(item);
            return;
        }
        
        applyRule(item);
    }

    private static void ProcessDefaultItem(Item item)
    {
        if (item.Quality > 0)
            item.Quality -= 1;
        
        item.SellIn -= 1;
        
        if (item.SellIn < 0 && item.Quality > 0)
            item.Quality -= 1;
    }
}