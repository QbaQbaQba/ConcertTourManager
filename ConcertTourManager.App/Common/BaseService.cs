using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcertTourManager.App.Abstract;
using ConcertTourManager.Domain.Common;

namespace ConcertTourManager.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public BaseService()
        {
            Items = new List<T>();
        }
        public List<T> Items { get; set; }
        public void AddItem(T item)
        {
            item.Id = GetLastId() + 1;
            Items.Add(item);
        }
        public void UpdateItem(T item)
        {

        }
        public void RemoveItem(T item)
        {
            var entity = Items.FirstOrDefault(p => p == item);
            if (entity != null)
            {
                Items.Remove(item);
            }
        }
        public int GetLastId()
        {
            int lastId;
            if (Items.Any())
            {
                lastId = Items.OrderBy(p => p.Id).LastOrDefault().Id;
            }
            else
            {
                lastId = 0;
            }
            return lastId;
        }
        public List<T> GetAllItems()
        {
            return Items;
        }
        public T GetItemById(int id)
        {
            var itemById = Items.FirstOrDefault(p => p.Id == id);
            if (itemById != null)
            {
                return itemById;
            }
            else
            {
                return null;
            }
        }
    }
}
