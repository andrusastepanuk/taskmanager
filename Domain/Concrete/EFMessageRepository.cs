using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Abstract;

namespace Domain.Concrete
{
    public class EFMessageRepository:IMessageRepository
    {
        private EFDbManagerContext context = new EFDbManagerContext();
        public IQueryable<Message> Messages
        {
            get
            {
                var messages = context.Messages.Include("From").Include("To");
                return messages;
            }
        }
        public void SaveMessage(Message message)
        {
            message.When = DateTime.Now;
            message.IsNew = true;
            context.Messages.Add(message);
            context.Entry(message).State = System.Data.EntityState.Added;
            context.SaveChanges();
            context.Messages.Include("From").Include("To").First(x => (x.ID == message.ID));
        }
        public void DeleteMessage(int messageID)
        {
            context.Messages.Remove(context.Messages.FirstOrDefault(x => (x.ID == messageID)));
            context.SaveChanges();
        }
        public void ChangeNew(int messageId)
        {
            context.Messages.FirstOrDefault(x => x.ID == messageId).IsNew = false;
            context.SaveChanges();
        }
    }
}
