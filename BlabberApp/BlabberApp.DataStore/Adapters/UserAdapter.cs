using System;
using System.Collections;
using BlabberApp.DataStore.Interfaces;
using BlabberApp.Domain.Entities;

namespace BlabberApp.DataStore.Adapters
{
    public class UserAdapter
    {
       private readonly IUserPlugin _plugin;

       public UserAdapter(IUserPlugin plugin)
       {
           _plugin = plugin;
       }

       public void Add(User user)
       {
            try
            {
                GetByEmail(user.Email.ToString());
            }
            catch(Exception)
            {
                _plugin.Create(user);
                return;
            }
            throw new Exception(user.Email.ToString() + " is a duplicate email");
       }

       public void Remove(User user)
       {
           _plugin.Delete(user);
       }

       public void Update(User user)
       {
           _plugin.Update(user);
       }

       public IEnumerable GetAll()
       {
           return _plugin.ReadAll();
       }

       public User GetById(Guid Id)
       {
           return (User)_plugin.ReadById(Id);
       }

        public User GetByEmail(string email)
        {
            try
            {
                User user = (User)_plugin.ReadByUserEmail(email);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}