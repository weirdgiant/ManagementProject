using System.Collections.ObjectModel;

namespace ManagementProject.Model
{
    /// <summary>
    /// TreeView绑定类别
    /// </summary>
    public class Category : INotifyPropertyChangedClass
    {
        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                NotifyPropertyChanged("CategoryName");
            }
        }

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                NotifyPropertyChanged("Products");
            }
        }
    }
    public class Product : INotifyPropertyChangedClass
    {
        private string modelName;
        public string ModelName
        {
            get { return modelName; }
            set
            {
                modelName = value;
                NotifyPropertyChanged("ModelName");
            }
        }
    }
}
