﻿using CQRS.Core;
using System;

namespace CQRS.Domain
{
    public class Client : AggregateRoot<IEvent>
    {
        private string name;

        public Client() : base(Guid.Empty)
        {
        }

        public Client(Guid id, string name) : base(id)
        {
            ApplyChange(new ClientCreatedEvent(Id, name));
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName)) throw new ArgumentException("Client name must be provided");
            ApplyChange(new ClientRenamedEvent(Id, newName));
        }

        public void Remove()
        {
            ApplyChange(new ClientRemovedEvent(Id));
        }

        private void Apply(ClientCreatedEvent e)
        {
            Id = e.Id;
            name = e.Name;
        }

        private void Apply(ClientRenamedEvent e)
        {
            name = e.NewName;
        }
    }

}
