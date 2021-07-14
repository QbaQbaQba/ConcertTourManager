using System;
using System.Collections.Generic;
using System.Text;

namespace ConcertTourManager.App.Abstract
{
    public interface IService<T>
    {
        List<T> Items { get; set; }
        void AddItem(T item);
        void RemoveItem(T item);
        void UpdateItem(T item);
        List<T> GetAllItems();
        int GetLastId();
        T GetItemById(int id);
    }
}
