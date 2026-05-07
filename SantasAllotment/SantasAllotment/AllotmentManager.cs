namespace SantasAllotment;

public class AllotmentManager(Allotment allotment)
{
    private int _treesLeft = allotment.NumberOfTrees;
    
    public char[][] ProcessAllotment()
    {
        PlaceTrees();
        return allotment.Slots;
    }
    
    private void PlaceTrees()
    {
        for (var column = 0; column < allotment.NumberOfTrees; column++)
        {
            for (var row = 0; row < allotment.NumberOfTrees; row++)
            {
                if (!CanPlaceTree(column, row))
                    continue;
                
                allotment.PlaceTree(column, row);
                _treesLeft--;
            }
        }

        if (_treesLeft > 0)
            allotment.ClearAllotment();
    }

    private bool CanPlaceTree(int column, int row)
    {
        if (allotment.SlotHasTree(column, row))
            return false;
        
        if (ProcessAllotmentCorners(column, row, out var canPlaceOnCorner))
            return canPlaceOnCorner;

        if (ProcessAllotmentEdges(column, row, out var canPlaceOnEdge))
            return canPlaceOnEdge;

        ProcessAllotmentCentralArea(column, row, out var canPlaceOnCentralArea);
        return canPlaceOnCentralArea;
    }

    private void ProcessAllotmentCentralArea(int column, int row, out bool canPlaceTree)
    {
        canPlaceTree = !allotment.SlotHasTree(column - 1, row - 1)
               && !allotment.SlotHasTree(column + 1, row - 1)
               && !allotment.SlotHasTree(column - 1, row + 1)
               && !allotment.SlotHasTree(column + 1, row + 1)
               && !allotment.SlotHasTree(column, row - 1)
               && !allotment.SlotHasTree(column, row + 1)
               && !allotment.SlotHasTree(column - 1, row)
               && !allotment.SlotHasTree(column + 1, row);
    }

    private bool ProcessAllotmentEdges(int column, int row, out bool canPlaceTree)
    {
        if (allotment.IsTop(row))
        {
            canPlaceTree = !allotment.SlotHasTree(column + 1, row + 1)
                           && !allotment.SlotHasTree(column - 1, row + 1)
                           && !allotment.SlotHasTree(column + 1, row)
                           && !allotment.SlotHasTree(column - 1, row)
                           && !allotment.SlotHasTree(column, row + 1);
            
            return true;
        }
        
        if (allotment.IsBottom(row))
        {
            canPlaceTree = allotment.SlotHasTree(column + 1, row - 1)
                           && allotment.SlotHasTree(column - 1, row - 1)
                           && allotment.SlotHasTree(column + 1, row)
                           && allotment.SlotHasTree(column - 1, row)
                           && allotment.SlotHasTree(column, row - 1);
            
            return true;
        }

        if (allotment.IsLeft(column))
        {
            canPlaceTree = allotment.SlotHasTree(column + 1, row + 1)
                           && allotment.SlotHasTree(column + 1, row - 1)
                           && allotment.SlotHasTree(column + 1, row)
                           && allotment.SlotHasTree(column, row - 1)
                           && allotment.SlotHasTree(column, row + 1);
            
            return true;
        }
        
        if (allotment.IsRight(column))
        {
            canPlaceTree = allotment.SlotHasTree(column - 1, row + 1)
                           && allotment.SlotHasTree(column - 1, row - 1)
                           && allotment.SlotHasTree(column - 1, row)
                           && allotment.SlotHasTree(column, row - 1)
                           && allotment.SlotHasTree(column, row + 1);
            
            return true;
        }

        canPlaceTree = false;
        return false;
    }

    private bool ProcessAllotmentCorners(int column, int row, out bool canPlaceTree)
    {
        if (allotment.IsTop(row) && allotment.IsLeft(column))
        {
            canPlaceTree = allotment.SlotHasTree(column + 1, row + 1)
                           && allotment.SlotHasTree(column, row + 1)
                           && allotment.SlotHasTree(column + 1, row);
            
            return true;
        }
        
        if (allotment.IsTop(row) && allotment.IsRight(column))
        {
            canPlaceTree = allotment.SlotHasTree(column - 1, row + 1)
                           && allotment.SlotHasTree(column, row + 1)
                           && allotment.SlotHasTree(column - 1, row);
            
            return true;
        }

        if (allotment.IsBottom(row) && allotment.IsLeft(column))
        {
            canPlaceTree = allotment.SlotHasTree(column + 1, row - 1)
                           && allotment.SlotHasTree(column, row - 1)
                           && allotment.SlotHasTree(column + 1, row);
            
            return true;
        }
        
        if (allotment.IsBottom(row) && allotment.IsRight(column))
        {
            canPlaceTree = allotment.SlotHasTree(column - 1, row - 1)
                           && allotment.SlotHasTree(column, row - 1)
                           && allotment.SlotHasTree(column - 1, row);
            
            return true;
        }
        
        canPlaceTree = false;
        return false;
    }
}