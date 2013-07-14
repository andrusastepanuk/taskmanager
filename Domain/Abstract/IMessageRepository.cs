using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IMessageRepository
    {
        IQueryable<Message> Messages { get; }
        void SaveMessage(Message message);
        void DeleteMessage(int messageID);
        void ChangeNew(int messageId);
    }
}
