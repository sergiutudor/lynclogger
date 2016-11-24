using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncLogger
{
    class ContactsManager
    {
        private LyncConnection connnection;

        public ContactsManager(LyncConnection Connection)
        {
            connnection = Connection;
        }

        public List<Contact> getAllContacts()
        {
            var client = connnection.getLyncClient();
            List<Contact> contacts = new List<Contact>();

            foreach (Group group in client.ContactManager.Groups)
            {
                foreach (Contact contact in group)
                {
                    if (!contacts.Contains(contact))
                        contacts.Add(contact);
                }
            }

            return contacts;
        }

        public bool isContactAvailable(Contact contact)
        {
            String availability = contact.GetContactInformation(ContactInformationType.Availability).ToString();

            if (availability == "3500")
            {
                return true;
            }

            return false;
        }
    }
}
