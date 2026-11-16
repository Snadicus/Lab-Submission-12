using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();

    private void Start()
    {
        // Create items
        GenerateInventory();

        // Test linear search
        InventoryItem foundItem = LinearSearchByName("Item 4");

        // Test binary search
        InventoryItem foundItem2 = BinarySearchByID(3);

        // Test quick sort
        QuickSortByValue();

        foreach (InventoryItem item in inventory)
        {
            Debug.Log(item.Name + " = Value " + item.Value);
        }
    }

    // Generate five inventory items with random IDs and Values
    private void GenerateInventory()
    {
        // Clear if already filled
        inventory.Clear();

        for (int i = 0; i < 5; i++)
        {
            // Give random values
            int randomID = Random.Range(1, 5);
            int randomValue = Random.Range(1, 5);

            // Create item
            InventoryItem item = new InventoryItem(randomID, "Item " + (i + 1), randomValue);

            // Add to list
            inventory.Add(item);
        }

        // Display list in debug
        DisplayInventory();
    }

    private void DisplayInventory()
    {
        foreach (InventoryItem item in inventory)
        {
            Debug.Log($"ID: {item.ID}, Name: {item.Name}, Value: {item.Value}");
        }
    }

    // Iterate through list until item is found or not
    public InventoryItem LinearSearchByName(string itemName)
    {
        foreach (InventoryItem item in inventory)
        {
            if (item.Name == itemName)
            {
                Debug.Log("Found " + item);
                return item;
            }
        }
        
        Debug.Log("Item not found");
        return null;
    }

    // Sorts ID lowest to highest
    private void SortInventoryByID()
    {
        inventory.Sort((a, b) => a.ID.CompareTo(b.ID));
    }

    // Binary search by item ID
    public InventoryItem BinarySearchByID(int itemID)
    {
        // Sort inventory before searching
        SortInventoryByID();

        int left = 0;
        int right = inventory.Count - 1;

        // Keep searching until there is no more search area 
        while (left <= right)
        {
            // Calc middle of search range
            int middle = left + (right - left) / 2;

            // Get item located in middle
            InventoryItem item = inventory[middle];

            // If ID matches, return
            // Otherwise keep looking 
            if (item.ID == itemID)
            {
                Debug.Log("Found " + item);
                return item;
            }
            else if (item.ID < itemID) // If smaller go right
            {
                left = middle + 1;
            }
            else // If larger go left
            {
                right = middle - 1;
            }
        }

        // If none match then return null

        Debug.Log("Item not found");
        return null;
    }

    // Quick Sort to sort inventory by value
    public void QuickSortByValue()
    {
        QuickSort(0, inventory.Count - 1);
    }

    // Recursive sort of inventory
    private void QuickSort(int low, int high)
    {
        // If section still contains more than one item
        if (low < high)
        {
            // Partition inventory
            int pivotIndex = Partition(low, high);

            // Sort items to left and right of pivot
            QuickSort(low, pivotIndex - 1);
            QuickSort(pivotIndex + 1, high);
        }
    }

    // Partition inventory around pivot
    private int Partition(int low, int high)
    {
        // Choose last item as pivot
        int pivot = inventory[high].Value;

        // Index for smaller or equal items
        int i = low - 1;

        // Check items before pivot
        for (int j = low; j < high; j++)
        {
            // If current item is smaller or equal than pivot then swap with current item.
            if (inventory[j].Value <= pivot)
            {
                i++;

                InventoryItem temp = inventory[i];
                inventory[i] = inventory[j];
                inventory[j] = temp;
            }
        }

        // Place pivot in correct position
        InventoryItem pivotItem = inventory[i + 1];
        inventory[i + 1] = inventory[high];
        inventory[high] = pivotItem;

        // Return sorted position

        return i + 1;
    }
}
