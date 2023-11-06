using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Core;
using WpfApp.Model;

namespace WpfApp.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public MainViewModel() 
        {
            Users = new ObservableCollection<User>
            {
                new User("1", "User1", "https://www.simplilearn.com/ice9/free_resources_article_thumb/what_is_image_Processing.jpg"),
                new User("2", "User2", "https://www.simplilearn.com/ice9/free_resources_article_thumb/what_is_image_Processing.jpg")
            };

            
            UserNameLabel = "User3";
            
        }

        //변수
        private string mUserNameLabel { get; set; }
        public string UserNameLabel 
        {
            get {return mUserNameLabel;}
            set { mUserNameLabel = value; OnPropertyChanged(nameof(UserNameLabel)); }
        }

        public ObservableCollection<Message> Messages { get; set; }
        public ObservableCollection<User> Users { get; set; }


    }
}
