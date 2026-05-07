namespace SantasAllotment;

public class Allotment
{
    public Allotment(int numberOfTrees)
    {
        NumberOfTrees = numberOfTrees;
        Slots = new char[numberOfTrees][];
        
        for (var column = 0; column < numberOfTrees; column++)
        {
            Slots[column] = new char[numberOfTrees];

            for (var row = 0; row < Slots[column].Length; row++)
            {
                Slots[column][row] = '-';
            }
        }
    }
    
    public int NumberOfTrees { get; private set; }
    public char[][] Slots { get; private set; }
    
    public bool IsTop(int row) => row == 0;
    public bool IsBottom(int row) => row == Slots.Length - 1;
    public bool IsLeft(int column) => column == 0;
    public bool IsRight(int column) => column == Slots.Length - 1;
    
    public bool SlotHasTree(int column, int row)
    {
        return Slots[column][row] == 'o';
    } 

    public void PlaceTree(int column, int row)
    {
        Slots[column][row] = 'o';   
    }

    public void ClearAllotment()
    {
        Slots = [];
    }
}