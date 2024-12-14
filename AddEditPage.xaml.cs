using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MuhamedovGlazki
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Agent currentAgent = new Agent();
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();
            if(SelectedAgent != null)
                currentAgent = SelectedAgent;
            DataContext = currentAgent;

            ComboType.ItemsSource = MuhamedovGlazkiSaveEntities.GetContext().AgentType.ToList();
            ComboType.DisplayMemberPath = "Title";
            ComboType.SelectedValuePath = "ID";
            ComboType.SelectedValue = currentAgent.AgentTypeID;
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
          OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if(myOpenFileDialog.ShowDialog() == true)
            {
                currentAgent.Logo = myOpenFileDialog.FileName;
                Logo.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (ComboType.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Title))
                errors.AppendLine("Укажите наименование агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Email))
                errors.AppendLine("Укажите e-mail агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Address))
                errors.AppendLine("Укажите адрес агента");
            if (string.IsNullOrWhiteSpace(currentAgent.DirectorName))
                errors.AppendLine("Укажите имя директора");
           
            if (string.IsNullOrWhiteSpace(currentAgent.INN))
                errors.AppendLine("Укажите ИНН агента");
            if (string.IsNullOrWhiteSpace(currentAgent.KPP))
                errors.AppendLine("Укажите КПП агента");
            if (currentAgent.Priority <= 0)
                errors.AppendLine("Укажите положительный приоритет агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Phone))
                errors.AppendLine("Укажите телефон агента");
            else
            {
                string ph = currentAgent.Phone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace("+", "");
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11 && (ph[0] == '7' || ph[0] == '0')) || (ph[1] == '3' && ph.Length != 12) && (ph[0] == '7' || ph[0] == '8'))
                    errors.AppendLine("Укажите правильно телефон агента");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (currentAgent.ID == 0)
            {
                MuhamedovGlazkiSaveEntities.GetContext().Agent.Add(currentAgent);
            }
            try
            {
                MuhamedovGlazkiSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
                
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentProductSale = MuhamedovGlazkiSaveEntities.GetContext().ProductSale.ToList();
            currentProductSale = currentProductSale.Where(p => p.AgentID == currentAgent.ID).ToList();

            if(currentProductSale.Count != 0)
            {
                MessageBox.Show("Невозможно выполнить удаление" );
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить эту запись", "Внимание!",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MuhamedovGlazkiSaveEntities.GetContext().Agent.Remove(currentAgent);
                    MuhamedovGlazkiSaveEntities.GetContext().SaveChanges();
                    Manager.MainFrame.GoBack();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString()) ;
                }
            }
        }
        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
