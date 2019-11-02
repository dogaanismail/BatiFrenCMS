using BatiFren.Business.Abstract;
using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
using System;
using System.Collections.Generic;

namespace BatiFren.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private IMessageDal _messageDal;
        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void Add(Message message)
        {
            _messageDal.Add(message);
        }

        public void Delete(Message message)
        {
            _messageDal.Delete(message);
        }

        public List<Message> GetAllLazyWithoutID()
        {
            return _messageDal.TolistInclude(x => x.User);
        }

        public Message GetByMessageId(int id)
        {
            return _messageDal.Find(x => x.MessageID == id);
        }

        public List<Message> GetList()
        {
            return _messageDal.GetList();
        }

        public void Update(Message message)
        {
             _messageDal.Update(message);
        }
    }
}
