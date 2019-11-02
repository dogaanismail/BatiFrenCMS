using BatiFren.Entities;
using System.Collections.Generic;

namespace BatiFren.Business.Abstract
{
    public interface IMessageService
    {
        List<Message> GetList();
        Message GetByMessageId(int id);
        void Add(Message message);
        void Delete(Message message);
        void Update(Message message);
        List<Message> GetAllLazyWithoutID();
    }
}
