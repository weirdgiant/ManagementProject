using ManagementProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.ViewModel
{
    public class LoginViewModel:LoginModel 
    {
        public DelegateCommand LoginOKCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public LoginViewModel()
        {
            Init();
            LoginOKCommand = new DelegateCommand();
            LoginOKCommand.ExecuteCommand = new Action<object>(LoginOK);

            CancelCommand = new DelegateCommand();
            CancelCommand.ExecuteCommand = new Action<object>(Cancel);
        }
        private void Init()
        {
            PhoneNumber = "021-65691583";
            MailAdress = "inquir@mangocosmos.com";
        }
        private void LoginOK(object obj)
        {
            if (obj != null)
            {
                Login login = (Login)obj;
                MainWindow main = new MainWindow();
                main.Show();
                login.Close();
            }
        }
        private void Cancel(object obj)
        {
            if (obj != null)
            {
                Login login = (Login)obj;
                login.Close();
            }
        }
        
    }
}
