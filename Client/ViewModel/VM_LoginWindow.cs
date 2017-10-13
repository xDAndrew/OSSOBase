using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    class VM_LoginWindow
    {
        View.LoginWindow WinLink;

        private List<Model.EF.Users> users;
        public List<Model.EF.Users> Users
        {
            get { return users; }
        }

        private Model.EF.Users selectedUser;
        public Model.EF.Users SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; }
        }

        private string userPassword;
        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }

        public VM_LoginWindow(View.LoginWindow WinLink)
        {
            this.WinLink = WinLink;
            users = Model.EF.EntityInstance.DBContext.UsersSet.Where(p => true).ToList();
        }

        private Command login;
        public Command Login
        {
            get
            {
                return login ?? (login = new Command(obj =>
                {
                    (new MainWindow()).Show();
                    WinLink.Close();
                }));
            }
        }
    }
}
